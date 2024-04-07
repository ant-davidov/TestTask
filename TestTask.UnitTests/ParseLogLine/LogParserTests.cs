using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Application.Services;

namespace TestTask.UnitTests.ParseLogLine
{
    public class LogParserTests
    {
        [Fact]
        public void ParseLogLine_ValidLogEntry_ReturnsLogEntry()
        {       
            string validLogEntry = "192.168.0.1:2024-04-06 12:30:45";
      
            var result = ParserLog.ParseLogLine(validLogEntry);

            Assert.NotNull(result);
            Assert.Equal("192.168.0.1", result.IPAddress.ToString());
            Assert.Equal(new DateTime(2024, 04, 06, 12, 30, 45), result.Time);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void ParseLogLine_NullOrEmptyLine_ReturnsNull(string line)
        {
            var result = ParserLog.ParseLogLine(line);

            Assert.Null(result);
        }

        [Theory]
        [InlineData("192.168.0.1")]
        [InlineData("192.168.0.1:2024-04-06")]
        [InlineData(":2024-04-06 12:30:45")]
        [InlineData("invalid_ip:2024-04-06 12:30:45")]
        public void ParseLogLine_InvalidLogEntryFormat_ReturnsNull(string line)
        {
            var result = ParserLog.ParseLogLine(line);

            Assert.Null(result);
        }

        [Theory]
        [InlineData("192.168.0.1:2024-24-06 12:30")]
        [InlineData("192.168.0.1:2024-04-06 12:30")]    
        public void ParseLogLine_ValidIPAddress_ReturnsNull(string line)
        {
            var result = ParserLog.ParseLogLine(line);

            Assert.Null(result);
        }

        [Theory]
        [InlineData("192.168.0.1:2024-04-06")]
        [InlineData("192.168.0.1:2024/04/06 12:30:45")]
        [InlineData("192.168.0.1:2024-04-06 12:30:45PM")]
        public void ParseLogLine_InvalidDateTimeFormat_ReturnsNull(string line)
        {
            var result = ParserLog.ParseLogLine(line);

            Assert.Null(result);
        }
    }
}
