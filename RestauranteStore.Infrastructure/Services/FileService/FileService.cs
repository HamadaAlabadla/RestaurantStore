using Microsoft.AspNetCore.Http;
using NToastNotify;

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
    }
}
