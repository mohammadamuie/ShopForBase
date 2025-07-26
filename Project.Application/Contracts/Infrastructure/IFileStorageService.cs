using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Project.Application.Contracts.Infrastructure
{
    public interface IFileStorageService
    {
        Task DeleteFile(string containerName, string fileRoute);
        Task<string> SaveFile(string containerName, IFormFile file);
        Task<string> EditFile(string containerName, IFormFile file, string fileRoute);
    }
}
