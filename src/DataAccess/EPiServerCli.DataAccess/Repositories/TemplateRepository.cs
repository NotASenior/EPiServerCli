using EPiServerCli.DataAccess.Interfaces.Repositories;
using EPiServerCli.DataAccess.Interfaces.Services;

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
            Validate(path, nameof(path));
            string finalPath = GetFinalPath(path);

            return fileService.ReadAllTextAsync(finalPath);
        }

        public Task CreateAsync(string path, string content)
        {
            Validate(path, nameof(path));
            Validate(content, nameof(content));
            string finalPath = GetFinalPath(path);

            return fileService.WriteAllTextAsync(finalPath, content);
        }
        
        private static void Validate(string parameter, string parameterName)
        {
            if (string.IsNullOrEmpty(parameter?.Trim()))
            {
                throw new ArgumentException(null, parameterName);
            }
        }

        private string GetFinalPath(string path)
        {
            string basePath = GetBasePath();
            return fileService.CombinePath(basePath, path);
        }

        private static string GetBasePath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
