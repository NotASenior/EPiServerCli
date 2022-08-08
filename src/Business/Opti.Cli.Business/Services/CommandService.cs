using Opti.Cli.Business.Interfaces.Factories;
using Opti.Cli.Business.Interfaces.Handlers;
using Opti.Cli.Business.Interfaces.Services;
using Opti.Cli.Domain.Entities;
using Opti.Cli.Domain.Mappers;

namespace Opti.Cli.Business.Services
{
    public class CommandService : ICommandService
    {
        private readonly ICommandHandlerFactory handlerFactory;
        private readonly ICommandMapper mapper;

        public CommandService(ICommandHandlerFactory handlerFactory, ICommandMapper mapper)
        {
            this.handlerFactory = handlerFactory;
            this.mapper = mapper;
        }

        public Task ExecuteAsync(string command)
        {
            Command mappedCommand = mapper.Map(command);
            ICommandHandler handler = handlerFactory.Get(mappedCommand);

            return handler.ExecuteAsync(mappedCommand);
        }
    }
}
