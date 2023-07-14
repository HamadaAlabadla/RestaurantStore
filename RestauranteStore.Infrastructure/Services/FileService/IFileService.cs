using Microsoft.AspNetCore.Http;

namespace RestauranteStore.Infrastructure.Services.FileService
{
	public interface IFileService
	{
		Task<string?> UploadFile(IFormFile ufile, string userName = "");
	}

}
