using Opti.Cli.DataAccess.Interfaces.Services;
using Opti.Cli.DataAccess.Services;
using System;
using Xunit;

namespace Opti.Cli.DataAccess.Tests.Services
{
    public class FileServiceTests
    {
        private readonly IFileService fileService;

        public FileServiceTests()
        {
            fileService = new FileService();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void Read_Invalid_ShouldThrow(string path)
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await fileService.ReadAllTextAsync(path));
        }

        [Theory]
        [InlineData(null, "")]
        [InlineData("", "")]
        [InlineData("    ", "")]
        [InlineData("path.cs", null)]
        [InlineData("path.cs", "")]
        [InlineData("path.cs", "    ")]
        public void Write_Invalid_ShouldThrow(string path, string content)
        {
            Assert.ThrowsAsync<ArgumentException>(async () => await fileService.WriteAllTextAsync(path, content));
        }
    }
}
