using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Application.Models;
using TestTask.Persistence;

namespace TestTask.UnitTests.IPFilter
{
    public class IpFilterTest
    {
        [Fact]
        public void Filter_ReturnsTrue_WhenLogInsideTimeFrameAndIPInRange()
        {
            var args = new Arguments
            {
                StartTime = "20.01.2024",
                EndTime = "22.01.2025",
                AddressStart = "192.168.0.0",
                AddressMask = 24
            };
            var logEntry = new LogEntry
            {
                Time = new DateTime(2024, 1, 21),
                IPAddress = System.Net.IPAddress.Parse("192.168.0.1")
            };

            var ipFilter = new IpFilter(args);
            var result = ipFilter.Filter(logEntry);

            Assert.True(result);
        }
        [Fact]
        public void Filter_ReturnsTrue_WhenIPInRange()
        {
            var args = new Arguments
            {
                StartTime = "20.01.2020",
                EndTime = "22.01.2025",
                AddressStart = "192.168.0.0",
                AddressMask = 24
            };
            var logEntry = new LogEntry
            {
                Time = new DateTime(2024, 1, 21),
                IPAddress = System.Net.IPAddress.Parse("192.168.0.255")
            };
            var logEntry2 = new LogEntry
            {
                Time = new DateTime(2024, 1, 21),
                IPAddress = System.Net.IPAddress.Parse("192.168.0.254")
            };
            var logEntry3 = new LogEntry
            {
                Time = new DateTime(2024, 1, 21),
                IPAddress = System.Net.IPAddress.Parse("192.168.0.0")
            };

            var ipFilter = new IpFilter(args);
            var result = ipFilter.Filter(logEntry);
            var result2 = ipFilter.Filter(logEntry2);   
            var result3 = ipFilter.Filter(logEntry3);

            Assert.True(result);
            Assert.True(result2);
            Assert.True(result3);
        }
        [Fact]
        public void Filter_ReturnsFalse_WhenIPNotInRange()
        {
            var args = new Arguments
            {
                StartTime = "20.01.2020",
                EndTime = "22.01.2025",
                AddressStart = "192.168.0.10",
                AddressMask = 28
            };
            var logEntry = new LogEntry
            {
                Time = new DateTime(2024, 1, 21),
                IPAddress = System.Net.IPAddress.Parse("192.168.0.16")
            };
            var logEntry2 = new LogEntry
            {
                Time = new DateTime(2024, 1, 21),
                IPAddress = System.Net.IPAddress.Parse("192.168.0.9")
            };
           

            var ipFilter = new IpFilter(args);
            var result = ipFilter.Filter(logEntry);
            var result2 = ipFilter.Filter(logEntry2);


            Assert.False(result);
            Assert.False(result2);
        }
        [Fact]
        public void Filter_ReturnsTrue_WhenIPInRangeAndMask0()
        {
            var args = new Arguments
            {
                StartTime = "20.01.2020",
                EndTime = "22.01.2025",
                AddressStart = "1.1.1.0",
                AddressMask = 0
            };
            var logEntry = new LogEntry
            {
                Time = new DateTime(2024, 1, 21),
                IPAddress = System.Net.IPAddress.Parse("1.1.1.1")
            };
            var logEntry2 = new LogEntry
            {
                Time = new DateTime(2024, 1, 21),
                IPAddress = System.Net.IPAddress.Parse("255.255.255.255")
            };


            var ipFilter = new IpFilter(args);
            var result = ipFilter.Filter(logEntry);
            var result2 = ipFilter.Filter(logEntry2);


            Assert.True(result);
            Assert.True(result2);
        }
        [Fact]
        public void Filter_ReturnsFalse_WhenIPNotInRangeAndMask32()
        {
            var args = new Arguments
            {
                StartTime = "20.01.2020",
                EndTime = "22.01.2025",
                AddressStart = "32.34.21.32",
                AddressMask = 32
            };
            var logEntry = new LogEntry
            {
                Time = new DateTime(2024, 1, 21),
                IPAddress = System.Net.IPAddress.Parse("32.34.21.33")
            };
            var logEntry2 = new LogEntry
            {
                Time = new DateTime(2024, 1, 21),
                IPAddress = System.Net.IPAddress.Parse("32.34.21.31")
            };


            var ipFilter = new IpFilter(args);
            var result = ipFilter.Filter(logEntry);
            var result2 = ipFilter.Filter(logEntry2);


            Assert.False(result);
            Assert.False(result2);
        }

        [Fact]
        public void Filter_ReturnsFalse_WhenLogOutsideTimeFrame()
        {
            var args = new Arguments
            {
                StartTime = "01.01.2025",
                EndTime = "01.01.2026",
                AddressStart = "192.168.0.0",
                AddressMask = 24
            };
            var logEntry = new LogEntry
            {
                Time = new DateTime(2024, 1, 15),
                IPAddress = System.Net.IPAddress.Parse("192.168.0.1")
            };
            var ipFilter = new IpFilter(args);

            var result = ipFilter.Filter(logEntry);

            Assert.False(result);
        }
        [Fact]
        public void Filter_ReturnsTrue_WhenLogBorderTimeFrame()
        {
            var args = new Arguments
            {
                StartTime = "20.01.2024",
                EndTime = "21.01.2024",
                AddressStart = "192.168.0.0",
                AddressMask = 24
            };

            var logEntry = new LogEntry
            {
                Time = new DateTime(2024, 1, 20, 0, 0, 0),
                IPAddress = System.Net.IPAddress.Parse("192.168.0.1")
            };
            var logEntry2 = new LogEntry
            {
                Time = new DateTime(2024, 1, 20, 23, 59, 59),
                IPAddress = System.Net.IPAddress.Parse("192.168.0.1")
            };
            var logEntry3 = new LogEntry
            {
                Time = new DateTime(2024, 1, 21, 0, 0, 0),
                IPAddress = System.Net.IPAddress.Parse("192.168.0.1")
            };
            var ipFilter = new IpFilter(args);

            var result = ipFilter.Filter(logEntry);
            var result2 = ipFilter.Filter(logEntry2);
            var result3 = ipFilter.Filter(logEntry3);

            Assert.True(result);
            Assert.True(result2);
            Assert.True(result3);
        }
        [Fact]
        public void Filter_ReturnsFalse_WhenLogBorderTimeFrame()
        {
            var args = new Arguments
            {
                StartTime = "20.01.2024",
                EndTime = "21.01.2024",
                AddressStart = "192.168.0.0",
                AddressMask = 24
            };
            var logEntry = new LogEntry
            {
                Time = new DateTime(2024, 1, 21,0,0,1),
                IPAddress = System.Net.IPAddress.Parse("192.168.0.1")
            };
            var ipFilter = new IpFilter(args);

            var result = ipFilter.Filter(logEntry);

            Assert.False(result);
        }
       
    }
}
