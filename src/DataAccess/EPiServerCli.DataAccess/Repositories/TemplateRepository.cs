using EPiServerCli.DataAccess.Interfaces.Repositories;
using EPiServerCli.DataAccess.Interfaces.Services;
using System.Reflection;

namespace EPiServerCli.DataAccess.Repositories
{
    public class TemplateRepository : ITemplateRepository
    {
        private readonly IFileService fileService;

        public TemplateRepository(IFileService fileService)
        {
            this.fileService = fileService;
        }

        public Task<string> GetAsync(string path)
        {
            if (string.IsNullOrEmpty(path?.Trim()))
            {
                throw new ArgumentException(nameof(path));
            }

            string basePath = GetBasePath();
            path = Path.Combine(basePath, path);

            return fileService.ReadAllTextAsync(path);
        }

        public Task CreateAsync(string path, string content)
        {
            if (string.IsNullOrEmpty(path?.Trim()))
            {
                throw new ArgumentException(nameof(path));
            }
            if (string.IsNullOrEmpty(content?.Trim()))
            {
                throw new ArgumentException(nameof(path));
            }

            string basePath = GetBasePath();
            path = Path.Combine(basePath, path);

            return fileService.WriteAllTextAsync(path, content);
        }

        private string GetBasePath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
