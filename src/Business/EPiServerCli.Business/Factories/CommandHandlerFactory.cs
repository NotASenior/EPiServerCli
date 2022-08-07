using EPiServerCli.Business.Handlers;
using EPiServerCli.Business.Interfaces.Factories;
using EPiServerCli.Business.Interfaces.Handlers;
using EPiServerCli.DataAccess.Interfaces.Repositories;
using EPiServerCli.Domain.Entities;

namespace EPiServerCli.Business.Factories
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
            }

            throw new ArgumentOutOfRangeException(nameof(command));
        }
    }
}
