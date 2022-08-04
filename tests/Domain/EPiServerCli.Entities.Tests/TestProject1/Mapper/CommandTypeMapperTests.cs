using EPiServerCli.Domain.Entities;
using EPiServerCli.Domain.Exceptions;
using EPiServerCli.Domain.Mappers;
using System;
using Xunit;

namespace EPiServerCli.Domain.Tests.Mappers
{
    public class CommandTypeMapperTests
    {
        private readonly ICommandTypeMapper mapper;

        public CommandTypeMapperTests()
        {
            this.mapper = new CommandTypeMapper();
        }

        [Theory]
        [InlineData("generate", CommandType.Generate)]
        public void Type_Ok_ShouldWork(string type, CommandType expected)
        {
            CommandType actual = mapper.Map(type);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(null)]
        public void Argument_Null_Throws(string type)
        {
            Assert.Throws<ArgumentNullException>(() => mapper.Map(type));
        }

        [Theory]
        [InlineData("")]
        [InlineData("      ")]
        public void Argument_Empty_Throws(string type)
        {
            Assert.Throws<ArgumentException>(() => mapper.Map(type));
        }

        [Theory]
        [InlineData("none")]
        public void Argument_Invalid_Throws(string type)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => mapper.Map(type));
        }
    }
}
