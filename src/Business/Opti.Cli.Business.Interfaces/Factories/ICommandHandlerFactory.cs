using Opti.Cli.Business.Interfaces.Handlers;
using Opti.Cli.Domain.Entities;

namespace Opti.Cli.Business.Interfaces.Factories
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler Get(Command command);
    }
}
