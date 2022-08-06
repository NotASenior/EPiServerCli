using EPiServerCli.Business.Interfaces.Factories;
using EPiServerCli.Business.Interfaces.Handlers;
using EPiServerCli.Business.Interfaces.Services;
using EPiServerCli.Domain.Entities;
using EPiServerCli.Domain.Mappers;

namespace EPiServerCli.Business.Services
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
