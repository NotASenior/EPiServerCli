using Opti.Cli.Business.Factories;
using Opti.Cli.Business.Handlers;
using Opti.Cli.Business.Interfaces.Factories;
using Opti.Cli.Business.Interfaces.Handlers;
using Opti.Cli.DataAccess.Repositories;
using Opti.Cli.DataAccess.Services;
using Opti.Cli.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Opti.Cli.Business.Tests.Factories
{
    public class CommandHandlerFactoryTests
    {
        private readonly ICommandHandlerFactory factory;

        public CommandHandlerFactoryTests()
        {
            var fileService = new FileService();
            var templateRepository = new TemplateRepository(fileService);
            this.factory = new CommandHandlerFactory(templateRepository);
        }

        [Fact]
        public void Command_Ok_ShouldWork()
        {
            var command = new Command(CommandType.Generate, ObjectType.Page,
                It.IsAny<string>(), 
                It.IsAny<IEnumerable<string>>(),
                It.IsAny<IEnumerable<string>>());

            ICommandHandler? handler = factory.Get(command);

            Assert.IsType<GeneratePageCommandHandler>(handler);
        }

        [Fact]
        public void Command_GenerateBlock_ShouldWork()
        {
            var command = new Command(CommandType.Generate, ObjectType.Block,
                It.IsAny<string>(),
                It.IsAny<IEnumerable<string>>(),
                It.IsAny<IEnumerable<string>>());

            ICommandHandler? handler = factory.Get(command);

            Assert.IsType<GenerateBlockCommandHandler>(handler);
        }

        [Fact]
        public void Command_GenerateSelectionFactory_ShouldWork()
        {
            var command = new Command(CommandType.Generate, ObjectType.SelectionFactory,
                It.IsAny<string>(),
                It.IsAny<IEnumerable<string>>(),
                It.IsAny<IEnumerable<string>>());

            ICommandHandler? handler = factory.Get(command);

            Assert.IsType<GenerateSelectionFactoryCommandHandler>(handler);
        }

        [Fact]
        public void Command_Invalid_ShouldThrow()
        {
            var command = new Command(CommandType.Generate, ObjectType.TemplateCoordinator,
                It.IsAny<string>(),
                It.IsAny<IEnumerable<string>>(),
                It.IsAny<IEnumerable<string>>());

            Assert.Throws<ArgumentOutOfRangeException>(() => factory.Get(command));
        }
    }
}
