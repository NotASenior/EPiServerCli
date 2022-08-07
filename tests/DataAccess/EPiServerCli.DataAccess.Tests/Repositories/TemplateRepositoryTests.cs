using EPiServerCli.DataAccess.Interfaces.Repositories;
using EPiServerCli.DataAccess.Interfaces.Services;
using EPiServerCli.DataAccess.Repositories;
using EPiServerCli.DataAccess.Services;
using System;
using Xunit;

namespace EPiServerCli.DataAccess.Tests.Services
{
    public class TemplateRepositoryTests
    {
        private readonly ITemplateRepository templateRepository;

        public TemplateRepositoryTests()
        {
            var fileService = new FileService();
            templateRepository = new TemplateRepository(fileService);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void Get_Invalid_ShouldThrow(string path)
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await templateRepository.GetAsync(path));
        }

        [Theory]
        [InlineData(null, "")]
        [InlineData("", "")]
        [InlineData("    ", "")]
        [InlineData("path.cs", null)]
        [InlineData("path.cs", "")]
        [InlineData("path.cs", "    ")]
        public void Create_Invalid_ShouldThrow(string path, string content)
        {
            Assert.ThrowsAsync<ArgumentException>(async () => await templateRepository.CreateAsync(path, content));
        }
    }
}
