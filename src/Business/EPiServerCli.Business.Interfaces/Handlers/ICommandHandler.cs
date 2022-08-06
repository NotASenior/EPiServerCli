using EPiServerCli.Domain.Entities;

namespace EPiServerCli.Business.Interfaces.Handlers
{
    public interface ICommandHandler
    {
        Task ExecuteAsync(Command command);
    }
}
