using Microsoft.AspNetCore.Http;

namespace ECommersionAPI.Application.Services
{
    public interface IFileService
    {
        Task<List<(string filename, string path)>> UploadAsync(string path,IFormFileCollection files);
        Task<string> FileRenameAsync(string fileName);
        Task<bool> CopyFileAsync(string path,IFormFile file);
    }
}
