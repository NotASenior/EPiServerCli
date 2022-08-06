namespace EPiServerCli.Business.Interfaces.Services
{
    public interface ICommandService
    {
        Task ExecuteAsync(string command);
    }
}
