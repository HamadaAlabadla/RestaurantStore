using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace RestaurantStore.Core.Validation
{
    public class SafeAndValidImage : ValidationAttribute
    {
        public string GetErrorMessage()
        {
            return $"The Image Not Safe !!!";
        }

        protected override ValidationResult? IsValid(
            object? value, ValidationContext validationContext)
        {
            if (value != null && value is IFormFile file)
            {
                if (!ImageProcessingService.ProcessImageAsync(file))
                {
                    return new ValidationResult(GetErrorMessage());
                }
                var maxSize = 20 * 1024;
                if (!IsImageValid(file) || !file.ContentType.StartsWith("image/"))
                {
                    return new ValidationResult("Please select an image file.");
                }
                if (!IsValidSizeImage(file, maxSize))
                {
                    return new ValidationResult($"Invalid file size. Max size is {maxSize} K");
                }
            }
            else
            {
                return new ValidationResult("this image not exist !");
            }


            return ValidationResult.Success;
        }


        private bool IsImageMalicious(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                bool extractedText = ContainsMaliciousContent(memoryStream.GetBuffer());
                Console.WriteLine("Extracted Text:");
                Console.WriteLine(extractedText);
                return extractedText;
            }
        }

        private bool ContainsMaliciousContent(byte[] content)
        {
            string imageContent = Encoding.UTF8.GetString(content);

            // Example: Search for a known malicious script pattern
            string maliciousPatternJS = @"<script.*?>|</script>";
            string maliciousPatternHTML = @"<html.*?>|</html>";
            string maliciousPatternSQL = @"SELECT\s|INSERT\s|UPDATE\s|DELETE\s";
            //string urlPattern = @"\b(http|https|www)\b";

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(imageContent);
            var script = Regex.IsMatch(imageContent, maliciousPatternJS, RegexOptions.IgnoreCase);
            var html = Regex.IsMatch(imageContent, maliciousPatternHTML, RegexOptions.IgnoreCase);
            var sql = Regex.IsMatch(imageContent, maliciousPatternSQL, RegexOptions.IgnoreCase);
            //var link = Regex.IsMatch(imageContent, urlPattern, RegexOptions.IgnoreCase);



            // Check for potential malicious HTML content

            if (script || html || sql
                ||
                ContainsMaliciousHTML(htmlDocument.DocumentNode))
            {
                Console.WriteLine("Potential malicious script detected.");
                return true;
            }


            return false; // Image is considered safe
        }

        static bool ContainsMaliciousHTML(HtmlNode node)
        {
            if (node.NodeType == HtmlNodeType.Element)
            {
                // Check for specific unsafe attributes or tags here
                // For example, checking for JavaScript events like "onclick"

                if (HasUnsafeAttributes(node) || IsUnsafeTag(node))
                {
                    return true;
                }
            }

            foreach (var childNode in node.ChildNodes)
            {
                if (ContainsMaliciousHTML(childNode))
                {
                    return true;
                }
            }

            return false;
        }


        static bool HasUnsafeAttributes(HtmlNode node)
        {
            // Implement your logic to check for unsafe attributes
            // For example, checking for "on*" attributes that might contain JavaScript code
            var unsafeAttributes = new[]
                {
                    "on*",       // JavaScript event handlers
					"src",       // Source for elements like <img> and <script>
					"href",      // Hyperlink reference
					"data",      // Data attributes that can be used for scripting
					"style",     // Inline styles
					"formaction" // Form action for <button> elements
					// Add more attributes as needed
				};

            foreach (var attribute in node.Attributes)
            {
                foreach (var unsafeAttr in unsafeAttributes)
                {
                    if (attribute.Name.StartsWith(unsafeAttr, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        static bool IsUnsafeTag(HtmlNode node)
        {
            // Implement your logic to check for unsafe tags
            // For example, checking for <script> or <iframe> tags

            var unsafeTags = new[] {
                "script",
                "iframe",
                "object",
                "embed",
                "form",
                "applet",
                "meta",
                "link",
                "style",
            };

            if (Array.IndexOf(unsafeTags, node.Name, 0, unsafeTags.Length) >= 0)
            {
                return true;
            }

            return false;
        }


        private static readonly Dictionary<string, byte[]> ImageSignatures = new Dictionary<string, byte[]>
        {
            { ".jpg", new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 } }, // JPEG
            { ".png", new byte[] { 0x89, 0x50, 0x4E, 0x47 } }, // PNG
            { ".gif", new byte[] { 0x47, 0x49, 0x46, 0x38 } }, // GIF
            { ".bmp", new byte[] { 0x42, 0x4D } },             // BMP
            { ".tiff", new byte[] { 0x49, 0x49, 0x2A, 0x00 } }, // TIFF (little-endian)
           // { ".tiff", new byte[] { 0x4D, 0x4D, 0x00, 0x2A } }, // TIFF (big-endian)
            // Add more image types and their signatures here
        };

        private static bool IsImageValid(IFormFile imageFile)
        {

            foreach (var validExtension in ImageSignatures.Keys)
            {
                byte[] signature = ImageSignatures[validExtension];
                byte[] buffer = new byte[signature.Length];

                using (var stream = imageFile.OpenReadStream())
                {
                    // Read the signature from the image stream
                    stream.Read(buffer, 0, signature.Length);
                }

                // Compare the read signature with the expected one
                if (buffer.SequenceEqual(signature))
                {
                    return true;
                }
            }

            return false; // No valid extension matched
        }

        private static bool IsValidSizeImage(IFormFile imageFile, double KmaxSize)
        {
            var maxAllowedSizeInBytes = KmaxSize * 1024;
            return imageFile.Length <= maxAllowedSizeInBytes;
        }


    }
}
