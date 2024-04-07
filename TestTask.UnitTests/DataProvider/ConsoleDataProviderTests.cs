using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Application.Services.DataProvider;

namespace TestTask.UnitTests.DataProvider
{
    public class ConsoleDataProviderTests
    {
        [Fact]
        public void GetData_WithValidArgs_ReturnsParsedArguments()
        {
            // Arrange
            string[] args = { "--file-log", "logfile.txt", "--address-start", "192.168.1.1" };
            var consoleDataProver = new ConsoleDataProvider(args);

            // Act
            var result = consoleDataProver.GetData();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("192.168.1.1", result.AddressStart);
            Assert.Equal("logfile.txt", result.FilePath);
        }

        [Fact]
        public void GetData_WithUnknownArgument_ThrowsArgumentException()
        {
            // Arrange
            string[] args = { "--invalid-arg" };
            var consoleDataProvider = new ConsoleDataProvider(args);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => consoleDataProvider.GetData());
        }

        [Fact]
        public void GetData_WithIncompleteArguments_ThrowsException()
        {
            // Arrange
            string[] args = { "--file-log" };
            var consoleDataProvider = new ConsoleDataProvider(args);

            // Act & Assert
            Assert.Throws<IndexOutOfRangeException>(() => consoleDataProvider.GetData());
        }
    }
}
