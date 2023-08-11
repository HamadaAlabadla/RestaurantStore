using Microsoft.AspNetCore.Http;
using NToastNotify;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace RestauranteStore.Infrastructure.Services.FileService
{
    public class FileService : IFileService
    {
        private readonly IToastNotification toastNotification;
        public FileService(IToastNotification toastNotification)
        {
            this.toastNotification = toastNotification;
        }
        public async Task<string?> UploadFile(IFormFile ufile, string path = "", string userName = "")
        {
            //var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff" };
            //var extension = Path.GetExtension(ufile.FileName).ToLower();

            //         if (allowedExtensions.Contains(extension))
            //         {
            if (IsImageValid(ufile))
            {
				
                    if (ufile != null && ufile.Length > 0)
                    {
                        if (ufile.ContentType.StartsWith("image/"))
                        {
                            var fileName = Path.GetFileName(ufile.FileName);
                            fileName = string.Concat(userName, " - ", fileName);
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), $@"wwwroot\images\" + path, fileName);
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await ufile.CopyToAsync(fileStream);
                            }
							string relativePath = Path.Combine("images", @"products" , fileName);
							string destinationPath = Path.Combine(@"wwwroot", relativePath);

							IsImageSizeValid(ufile, destinationPath);

							return fileName;
                        }
                        else
                        {
                            toastNotification.AddWarningToastMessage("Invalid file format");
                        }
                    }
                    else
                    {
                        toastNotification.AddWarningToastMessage("Invalid file format");
                    }

            }
            else
            {
				toastNotification.AddWarningToastMessage("Please select an image file.");
			}
			return null;
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
			try
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
			}
			catch(Exception ex)
			{

			}

			return false; // No valid extension matched
		}


		private static bool IsImageSizeValid(IFormFile imageFile , string destinationPath)
		{
            var maxAllowedSizeInBytes = 20 * 1024;
			//return imageFile.Length <= maxAllowedSizeInBytes;
			using (var image = Image.Load(imageFile.OpenReadStream()))
			{
				int quality = 85; // You can adjust the image quality (0 to 100)

				using (var outputStream = new MemoryStream())
				{
					image.Save(outputStream, new JpegEncoder { Quality = quality });

					if (outputStream.Length <= maxAllowedSizeInBytes)
					{
						File.WriteAllBytes(destinationPath, outputStream.ToArray());
                        return true;
					}
					else
					{
						double scaleFactor = Math.Sqrt((double)(maxAllowedSizeInBytes) /( outputStream.Length + 10000000));
						int newWidth = (int)(image.Width * scaleFactor);
						int newHeight = (int)(image.Height * scaleFactor);

						var resizedImage = image.Clone(ctx => ctx.Resize(newWidth, newHeight));

						using (var resizedStream = new MemoryStream())
						{
							resizedImage.Save(resizedStream, new JpegEncoder { Quality = quality });

							if (resizedStream.Length <= maxAllowedSizeInBytes)
							{
								File.WriteAllBytes(destinationPath, resizedStream.ToArray());
								return true;
							}
						}
					}
				}
			}
			return false;

		}
	}
}
