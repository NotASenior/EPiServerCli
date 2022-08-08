using Opti.Cli.DataAccess.Interfaces.Services;

namespace Opti.Cli.DataAccess.Services
{
    public class FileService : IFileService
    {
        public string CombinePath(string basePath, string path)
        {
            return Path.Combine(basePath, path);
        }

        public Task<string> ReadAllTextAsync(string path)
        {
            Validate(path, nameof(path));

            return File.ReadAllTextAsync(path);
        }

        public Task WriteAllTextAsync(string path, string content)
        {
            Validate(path, nameof(path));
            Validate(content, nameof(content));

            Directory.CreateDirectory(Path.GetDirectoryName(path)!);

            return File.WriteAllTextAsync(path, content);
        }

        private static void Validate(string parameter, string parameterName)
        {
            if (string.IsNullOrEmpty(parameter?.Trim()))
            {
                throw new ArgumentException(null, parameterName);
            }
        }
    }
}
