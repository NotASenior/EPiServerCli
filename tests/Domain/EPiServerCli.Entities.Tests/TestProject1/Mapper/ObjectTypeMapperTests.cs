using EPiServerCli.Domain.Entities;
using EPiServerCli.Domain.Exceptions;
using EPiServerCli.Domain.Mappers;
using System;
using Xunit;

namespace EPiServerCli.Domain.Tests.Mappers
{
    public class ObjectTypeMapperTests
    {
        private readonly IObjectTypeMapper mapper;

        public ObjectTypeMapperTests()
        {
            this.mapper = new ObjectTypeMapper();
        }

        [Theory]
        [InlineData("page", ObjectType.Page)]
        [InlineData("block", ObjectType.Block)]
        public void Type_Ok_ShouldWork(string type, ObjectType expected)
        {
            ObjectType actual = mapper.Map(type);

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
