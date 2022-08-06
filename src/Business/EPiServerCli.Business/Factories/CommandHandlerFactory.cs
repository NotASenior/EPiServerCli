using EPiServerCli.Business.Handlers;
using EPiServerCli.Business.Interfaces.Factories;
using EPiServerCli.Business.Interfaces.Handlers;
using EPiServerCli.Domain.Entities;

namespace EPiServerCli.Business.Factories
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        public ICommandHandler Get(Command command)
        {
            switch (command.Type, command.ObjectType)
            {
                case (CommandType.Generate, ObjectType.Page): return new GeneratePageCommandHandler();
                case (CommandType.Generate, ObjectType.Block): return new GenerateBlockCommandHandler();
            }

            throw new ArgumentOutOfRangeException(nameof(command));
        }
    }
}
