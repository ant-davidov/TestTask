//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace TestTask.Application.Models
//{
//    internal static class ValidationArgs2
//    {
//        public static void Validate(Arguments args)
//        {
//            ValidateFilePath(args.FilePath);
//            ValidateOutputFilePath(args.OutputFilePath);
//            ValidateIPandMask(args.AddressStart, args.AddressMask);
//            VaidateTime(args.StartTime, args.EndTime);


//        }

//        private static void ValidateFilePath(string filePath)
//        {
//            if (string.IsNullOrEmpty(filePath))
//                throw new Exception("Path is null");
//            if (!File.Exists(filePath))
//                throw new FileNotFoundException($"Not found {filePath}");
//        }

//        private static void ValidateOutputFilePath(string filePath)
//        {
//            if (string.IsNullOrEmpty(filePath))
//                throw new Exception("Empty path for save");
//            string directory = Path.GetDirectoryName(filePath);
//            if (string.IsNullOrEmpty(directory) || !Directory.Exists(directory) || directory == "\\")
//                throw new Exception($"There is no directory for saving the file - {filePath}");
//            if (Directory.Exists(filePath))
//                throw new Exception($"{filePath} is a folder");
//        }

//        private static void ValidateIPandMask(string ip, int? mask)
//        {
//            var ipIsnull = string.IsNullOrEmpty(ip);
//            if (ipIsnull && mask != null)
//                throw new ArgumentException("The --address-mask cannot be used if address-start is not specified");
//            if (ipIsnull) return;
//            System.Net.IPAddress ipAddress = null;
//            bool isValidIp = System.Net.IPAddress.TryParse(ip, out ipAddress);
//            if (!isValidIp)
//                throw new InvalidCastException($"{ip} is not an ip address");
//            if (mask != null && (mask > 32 || mask < 0))
//                throw new InvalidCastException($"Mask({mask}) should be 0 - 32 ");
//        }

//        private static void VaidateTime(string startTime, string endTime)
//        {
//            if (DateTime.TryParseExact(startTime, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var startDate) && DateTime.TryParseExact(endTime, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var endDate))
//            {
//                if (endDate < startDate)
//                    throw new ArgumentException("The end date is less than the start date");
//                return;
//            }
//            throw new InvalidCastException("Invalid date format. try --time-start and --time-end dd.MM.yyyy");
//        }
//    }
//}
