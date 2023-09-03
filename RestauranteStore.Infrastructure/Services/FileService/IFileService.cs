using Microsoft.AspNetCore.Http;

namespace RestaurantStore.Infrastructure.Services.FileService
{
    public interface IFileService
    {
        Task<string?> UploadFile(IFormFile ufile, string path = "", string userName = "");
    }

}
