using EPiServerCli.DataAccess.Interfaces.Services;

namespace EPiServerCli.DataAccess.Services
{
    public class FileService : IFileService
    {
        public Task<string> ReadAllTextAsync(string path)
        {
            if (string.IsNullOrEmpty(path?.Trim()))
            {
                throw new ArgumentException(nameof(path));
            }

            return File.ReadAllTextAsync(path);
        }

        public Task WriteAllTextAsync(string path, string content)
        {
            if (string.IsNullOrEmpty(path?.Trim()))
            {
                throw new ArgumentException(nameof(path));
            }
            if (string.IsNullOrEmpty(content?.Trim()))
            {
                throw new ArgumentException(nameof(content));
            }
            
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);

            return File.WriteAllTextAsync(path, content);
        }
    }
}
