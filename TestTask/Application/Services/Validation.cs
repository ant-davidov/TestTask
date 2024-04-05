using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Application.Models;

namespace TestTask.Application.Services
{
    internal static class Validation
    {
        public static void validate(Arguments args)
        {
            validateFilePath(args.FilePath);
            validateOutputFilePath(args.OutputFilePath);
            validateIPandMask(args.AddressStart, args.AddressMask);
        }

        private static void validateFilePath (string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Not found {filePath}");        
        }

        private static void validateOutputFilePath(string filePath) 
        { 

        }

        private static void validateIPandMask(string ip, int? mask) 
        {
            if (ip == null) return;
            System.Net.IPAddress ipAddress = null;
            bool isValidIp = System.Net.IPAddress.TryParse(ip, out ipAddress);
            if (!isValidIp)
                throw new InvalidCastException($"{ip} is not an ip address");
            if (mask != null &&(mask > 32 || mask < 0))
                throw new InvalidCastException($" mask({mask}) should be 0 - 32 ");
        }
    }
}
