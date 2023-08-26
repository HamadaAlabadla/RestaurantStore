using Microsoft.AspNetCore.Http;
using NToastNotify;
//using System.Drawing;
//using System.Reflection.Metadata;
//using Tesseract;

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
			if (ufile != null && ufile.Length > 0)
			{
				if (ufile.ContentType.StartsWith("image/"))
				{


					//CheckOCR(ufile);
					var fileName = Path.GetFileName(ufile.FileName);
					fileName = string.Concat(userName, " - ", fileName);
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), $@"wwwroot\images\" + path, fileName);
					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						await ufile.CopyToAsync(fileStream);
					}
					string relativePath = Path.Combine("images", @"products", fileName);
					string destinationPath = Path.Combine(@"wwwroot", relativePath);



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
			return null;
		}



		//private static bool IsImageSizeValid(IFormFile imageFile , string destinationPath)
		//{
		//          var maxAllowedSizeInBytes = 20 * 1024;
		//	//return imageFile.Length <= maxAllowedSizeInBytes;
		//	using (var image = Image.Load(imageFile.OpenReadStream()))
		//	{
		//		int quality = 85; // You can adjust the image quality (0 to 100)

		//		using (var outputStream = new MemoryStream())
		//		{
		//			image.Save(outputStream, new JpegEncoder { Quality = quality });

		//			if (outputStream.Length <= maxAllowedSizeInBytes)
		//			{
		//				File.WriteAllBytes(destinationPath, outputStream.ToArray());
		//                      return true;
		//			}
		//			else
		//			{
		//				double scaleFactor = Math.Sqrt((double)(maxAllowedSizeInBytes) /( outputStream.Length + 10000000));
		//				int newWidth = (int)(image.Width * scaleFactor);
		//				int newHeight = (int)(image.Height * scaleFactor);

		//				var resizedImage = image.Clone(ctx => ctx.Resize(newWidth, newHeight));

		//				using (var resizedStream = new MemoryStream())
		//				{
		//					resizedImage.Save(resizedStream, new JpegEncoder { Quality = quality });

		//					if (resizedStream.Length <= maxAllowedSizeInBytes)
		//					{
		//						File.WriteAllBytes(destinationPath, resizedStream.ToArray());
		//						return true;
		//					}
		//				}
		//			}
		//		}
		//	}
		//	return false;

		//}


	}

}
