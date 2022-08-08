using EPiServerCli.Domain.Entities;
using EPiServerCli.Domain.Exceptions;
using EPiServerCli.Domain.Mappers;
using System;
using System.Linq;
using Xunit;

namespace EPiServerCli.Domain.Tests.Mappers
{
    public class CommandMapperTests
    {
        private readonly ICommandMapper mapper;

        public CommandMapperTests()
        {
            this.mapper = new CommandMapper(new ObjectTypeMapper(), new CommandTypeMapper());
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
    }
}