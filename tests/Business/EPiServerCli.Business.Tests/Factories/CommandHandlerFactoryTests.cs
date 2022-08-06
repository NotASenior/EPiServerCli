using EPiServerCli.Business.Factories;
using EPiServerCli.Business.Handlers;
using EPiServerCli.Business.Interfaces.Factories;
using EPiServerCli.Business.Interfaces.Handlers;
using EPiServerCli.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace EPiServerCli.Business.Tests.Factories
{
    public class CommandHandlerFactoryTests
    {
        private readonly ICommandHandlerFactory factory;

        public CommandHandlerFactoryTests()
        {
            this.factory = new CommandHandlerFactory();
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
        public void Command_Invalid_ShouldThrow()
        {
            var command = new Command(CommandType.Generate, ObjectType.SelectionFactory,
                It.IsAny<string>(),
                It.IsAny<IEnumerable<string>>(),
                It.IsAny<IEnumerable<string>>());

            Assert.Throws<ArgumentOutOfRangeException>(() => factory.Get(command));
        }
    }
}
