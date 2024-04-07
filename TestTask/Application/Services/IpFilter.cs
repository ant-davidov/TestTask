using System.Globalization;
using System.Net;
using TestTask.Application.Models;
using TestTask.Persistence;

internal class IpFilter
{
    private readonly IPAddress? _minIpAddress;
    private readonly int _mask = -1;
    private readonly uint _minIpInBit;
    private readonly uint _maxIpInBit;
    private readonly DateTime _startTime;
    private readonly DateTime _endTime;
    public IpFilter(Arguments args)
    {
        _startTime = DateTime.ParseExact(args.StartTime, "dd.MM.yyyy", CultureInfo.InvariantCulture);
        _endTime = DateTime.ParseExact(args.EndTime, "dd.MM.yyyy", CultureInfo.InvariantCulture);
        if (!String.IsNullOrEmpty(args.AddressStart))
        {
            _minIpAddress = IPAddress.Parse(args.AddressStart);
            _minIpInBit = ByteToUint(_minIpAddress.GetAddressBytes());
            _mask = args.AddressMask ?? -1;
            _maxIpInBit = GetMaxIp();

        }
        else
            _minIpAddress = null;
    }
    public bool Filter(LogEntry log)
    {
        if (log.Time > _endTime || log.Time < _startTime)
            return false;
        return IpEntersInSegment(log);
    }
    private bool IpEntersInSegment(LogEntry log)
    {
        if (_minIpAddress == null)
            return true;
        uint ipBite = ByteToUint(log.IPAddress.GetAddressBytes());
        if (ipBite >= _minIpInBit && ipBite <= _maxIpInBit)
            return true;
        return false;
    }
    private uint GetMaxIp()
    {
        if (-1 == _mask)
            return uint.MaxValue;
        if (32 == _mask)
            return _minIpInBit;
        if (0 == _mask)
            return uint.MaxValue;

        uint mask = (uint)(0xFFFFFFFF << (32 - _mask));
        uint minip = _minIpInBit & mask;
        uint invertedMask = ~mask;
        uint maxIpInBit = minip | invertedMask;
        byte[] maxIpBytes = BitConverter.GetBytes(maxIpInBit);
        if (BitConverter.IsLittleEndian)
            Array.Reverse(maxIpBytes);
        return ByteToUint(maxIpBytes);
    }

    private uint ByteToUint(byte[] bytes)
    {
        var ip2 = (uint)bytes[3];
        ip2 += (uint)bytes[2] << 8;
        ip2 += (uint)bytes[1] << 16;
        ip2 += (uint)bytes[0] << 24;
        return ip2;
    }
}
