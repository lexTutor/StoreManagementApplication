using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace StoreApp.NetFramework.Services
{
    public interface IImageService
    {
        string UploadImage(IFormFile model);
    }
}