using System.Globalization;
using System.Net;
using TestTask.Persistence;

namespace TestTask.Application.Services
{
    internal static class ParserLog
    {
        public static LogEntry ParseLogLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                Console.WriteLine("Input line is null or empty. Skip line");
                return null;
            }

            var splitLine = line.Split(':', 2);

            if (splitLine.Length != 2)
            {
                Console.WriteLine("Invalid log entry format. Expected 'IP_Address:DateTime' Skip line");
                return null;
            }

            IPAddress ip;
            var isValidIp = IPAddress.TryParse(splitLine[0], out ip);
            if (!isValidIp)
            {
                Console.WriteLine($"Not Valid ip {splitLine[0]} skip line");
                return null;
            }

            if (DateTime.TryParseExact(splitLine[1], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var time))
                return new LogEntry { IPAddress = ip, Time = time };
            Console.WriteLine($"Not Valid datetime {splitLine[1]} skip line, ip - {splitLine[0]}");
            return null;

        }
    }
}
