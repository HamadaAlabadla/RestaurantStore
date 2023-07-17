using Microsoft.AspNetCore.Http;

namespace RestauranteStore.Infrastructure.Services.FileService
{
	public class FileService : IFileService
	{
		public async Task<string?> UploadFile(IFormFile ufile, string path ="", string userName = "")
		{
			if (ufile != null && ufile.Length > 0)
			{
				var fileName = Path.GetFileName(ufile.FileName);
				fileName = string.Concat(userName, " - ", fileName);
				var filePath = Path.Combine(Directory.GetCurrentDirectory(), $@"wwwroot\images\"+path, fileName);
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					await ufile.CopyToAsync(fileStream);
				}
				return fileName;
			}
			return null;
		}
	}
}
