using EPiServerCli.Business.Interfaces.Handlers;
using EPiServerCli.Domain.Entities;

namespace EPiServerCli.Business.Interfaces.Factories
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler Get(Command command);
    }
}
