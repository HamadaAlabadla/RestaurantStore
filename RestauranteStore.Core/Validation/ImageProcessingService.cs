using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using Microsoft.AspNetCore.Http;

namespace RestaurantStore.Core.Validation
{
    public class ImageProcessingService
    {
        public static bool ProcessImageAsync(IFormFile imageFile)
        {
            // Extract image size
            long exifProfile = GetImageSizeAsync(imageFile);

            // Convert image to bytes
            byte[] imageBytes = ConvertImageToBytesAsync(imageFile);

            if (exifProfile < (imageBytes.Length - (exifProfile / 4)))
                return false;
            return true;
        }

        private static long GetImageSizeAsync(IFormFile imageFile)
        {
            using (var imageStream = imageFile.OpenReadStream())
            {
                var metadataDirectories = ImageMetadataReader.ReadMetadata(imageStream);
                var exifDirectory = metadataDirectories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
                var val = metadataDirectories.FirstOrDefault()!.Tags;
                int width = exifDirectory?.GetInt32(ExifDirectoryBase.TagImageWidth) ?? 0;
                int height = exifDirectory?.GetInt32(ExifDirectoryBase.TagImageHeight) ?? 0;
                width = int.Parse(((val.First(x => x.Name.Equals("Image Width")).Description ?? "").Split(' '))[0]);
                height = int.Parse(((val.First(x => x.Name.Equals("Image Height")).Description ?? "").Split(' '))[0]);
                int bitsPerPixel = 24; // Assuming 24 bits per pixel for simplicity

                // Calculate the size in bytes
                long sizeInBytes = (width * height) / 8;

                return sizeInBytes;
            }
        }


        private static byte[] ConvertImageToBytesAsync(IFormFile imageFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                imageFile.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
