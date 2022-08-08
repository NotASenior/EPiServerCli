using Opti.Cli.Domain.Entities;
using Opti.Cli.Domain.Exceptions;
using Opti.Cli.Domain.Mappers;
using System;
using System.Collections.Generic;
using Xunit;

namespace Opti.Cli.Domain.Tests.Mappers
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

        [Theory]
        [InlineData("p", ObjectType.Page)]
        [InlineData("pa", ObjectType.Page)]
        [InlineData("pag", ObjectType.Page)]
        [InlineData("page", ObjectType.Page)]
        [InlineData("b", ObjectType.Block)]
        [InlineData("bl", ObjectType.Block)]
        [InlineData("blo", ObjectType.Block)]
        [InlineData("bloc", ObjectType.Block)]
        [InlineData("block", ObjectType.Block)]
        public void Argument_NotCompleted_ShouldAutocomplete(string type, ObjectType expected)
        {
            ObjectType actual = mapper.Map(type);

            Assert.Equal(expected, actual);
        }
    }
}
