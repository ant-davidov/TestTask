using System.Net;

namespace TestTask.Persistence
{
    class LogEntry
    {
        public IPAddress IPAddress { get; set; } = null!;
        public DateTime Time { get; set; }
        public int Size { get; private set; }
        public void IncreaseSize()
            => Size += 1;
        public override int GetHashCode()
        {
            if (IPAddress == null || IPAddress.Address == 0) return 0;
            return IPAddress.GetHashCode();
        }
        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is LogEntry otherLogEntry))
                return false;
            return this.IPAddress == otherLogEntry.IPAddress;
        }
    }

}
