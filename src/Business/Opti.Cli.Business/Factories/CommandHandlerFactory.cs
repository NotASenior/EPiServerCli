using Opti.Cli.Business.Handlers;
using Opti.Cli.Business.Interfaces.Factories;
using Opti.Cli.Business.Interfaces.Handlers;
using Opti.Cli.DataAccess.Interfaces.Repositories;
using Opti.Cli.Domain.Entities;

namespace Opti.Cli.Business.Factories
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly ITemplateRepository templateRepository;

        public CommandHandlerFactory(ITemplateRepository templateRepository)
        {
            this.templateRepository = templateRepository;
        }

        public ICommandHandler Get(Command command)
        {
            switch (command.Type, command.ObjectType)
            {
                case (CommandType.Generate, ObjectType.Page): return new GeneratePageCommandHandler(templateRepository);
                case (CommandType.Generate, ObjectType.Block): return new GenerateBlockCommandHandler(templateRepository);
                case (CommandType.Generate, ObjectType.SelectionFactory): return new GenerateSelectionFactoryCommandHandler(templateRepository);
            }

            throw new ArgumentOutOfRangeException(nameof(command));
        }
    }
}
