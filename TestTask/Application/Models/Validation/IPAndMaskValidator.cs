using TestTask.Application.Interfaces;

namespace TestTask.Application.Models.Validation
{
    internal class IPAndMaskValidator : IValidator
    {
        public void Validate(Arguments args)
        {
            string ip = args.AddressStart;
            int? mask = args.AddressMask;
            var ipIsnull = string.IsNullOrEmpty(ip);
            if (ipIsnull && mask != null)
                throw new ArgumentException("The --address-mask cannot be used if address-start is not specified");
            if (ipIsnull) return;
            System.Net.IPAddress ipAddress = null;
            bool isValidIp = System.Net.IPAddress.TryParse(ip, out ipAddress);
            if (!isValidIp)
                throw new InvalidCastException($"{ip} is not an ip address");
            if (mask != null && (mask > 32 || mask < 0))
                throw new InvalidCastException($"Mask({mask}) should be 0 - 32 ");
        }
    }
}
