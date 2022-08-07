namespace EPiServerCli.DataAccess.Interfaces.Repositories
{
    public interface ITemplateRepository
    {
        Task<string> GetAsync(string path);
        Task CreateAsync(string path, string content);
    }
}
