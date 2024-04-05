using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Persistence
{
    class LogEntry
    {
        public string IPAddress { get; set; }
        public DateTime MinTime { get; set; }
        public DateTime MaxTime { get; set; }
        public int size { get; set; }

        public override int GetHashCode()
        {
            return IPAddress.GetHashCode();
        }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (!(obj is LogEntry)) return false;
            if  (this.IPAddress != ((LogEntry)obj).IPAddress) return false;
            return true;
        }
    }
   
}
