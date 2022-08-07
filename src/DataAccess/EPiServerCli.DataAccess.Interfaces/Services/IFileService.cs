namespace EPiServerCli.DataAccess.Interfaces.Services
{
    public interface IFileService
    {
        Task<string> ReadAllTextAsync(string path);
        Task WriteAllTextAsync(string path, string content);
        string CombinePath(string basePath, string path);
    }
}
