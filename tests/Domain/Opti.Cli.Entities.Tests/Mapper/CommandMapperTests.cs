using Opti.Cli.Domain.Entities;
using Opti.Cli.Domain.Exceptions;
using Opti.Cli.Domain.Mappers;
using System;
using System.Linq;
using Xunit;

namespace Opti.Cli.Domain.Tests.Mappers
{
    public class CommandMapperTests
    {
        private readonly ICommandMapper mapper;

        public CommandMapperTests()
        {
            this.mapper = new CommandMapper(new ObjectTypeMapper(), new CommandTypeMapper());
        }

        [Fact]
        public void Command_BlockOk_ShouldWork()
        {
            Command command = mapper.Map("generate block TestBlock");

            Assert.NotNull(command);
            Assert.Equal(CommandType.Generate, command.Type);
            Assert.Equal(ObjectType.Block, command.ObjectType);
            Assert.Equal("TestBlock", command.Name);
            Assert.NotNull(command.Path);
            Assert.NotNull(command.Options);
            Assert.Empty(command.Path);
            Assert.Empty(command.Options);
        }

        [Fact]
        public void Command_Ok_ShouldWork()
        {
            Command command = mapper.Map("generate page TestPage");

            Assert.NotNull(command);
            Assert.Equal(CommandType.Generate, command.Type);
            Assert.Equal(ObjectType.Page, command.ObjectType);
            Assert.Equal("TestPage", command.Name);
            Assert.NotNull(command.Path);
            Assert.NotNull(command.Options);
            Assert.Empty(command.Path);
            Assert.Empty(command.Options);
        }

        [Theory]
        [InlineData(null)]
        public void Argument_Null_Throws(string command)
        {
            Assert.Throws<ArgumentNullException>(() => mapper.Map(command));
        }

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        public void Argument_Invalid_Throws(string command)
        {
            Assert.Throws<ArgumentException>(() => mapper.Map(command));
        }

        [Theory]
        [InlineData("generate")]
        [InlineData("generate page")]
        public void Command_Invalid_Throws(string command)
        {
            Assert.Throws<CommandNotValidException>(() => mapper.Map(command));
        }

        [Fact]
        public void Command_WithoutSuffix_ShouldAutocomplete()
        {
            string command = "generate page Test";
            Command mappedCommand = mapper.Map(command);

            Assert.Equal("TestPage", mappedCommand.Name);
        }

        [Fact]
        public void Block_WithoutSuffix_ShouldAutocomplete()
        {
            string command = "generate block Test";
            Command mappedCommand = mapper.Map(command);

            Assert.Equal("TestBlock", mappedCommand.Name);
        }

        [Fact]
        public void SelectionFactory_WithoutSuffix_ShouldAutocomplete()
        {
            string command = "generate sf Test";
            Command mappedCommand = mapper.Map(command);

            Assert.Equal("TestSelectionFactory", mappedCommand.Name);
        }

        [Fact]
        public void InitializableModule_WithoutSuffix_ShouldAutocomplete()
        {
            string command = "generate im Test";
            Command mappedCommand = mapper.Map(command);

            Assert.Equal("TestInitialization", mappedCommand.Name);
        }
    }
}