using System.Net;

namespace TestTask.Persistence
{

    internal class Repository
    {
        private static Dictionary<IPAddress, uint>? _logEntries;
        public Repository()
        {
            _logEntries = new Dictionary<IPAddress, uint>();
        }
        public void Add(LogEntry entry)
        {

            if (_logEntries!.ContainsKey(entry.IPAddress))
            {
                entry.IncreaseSize();
                _logEntries[entry.IPAddress] += 1;
            }
            else
            {
                _logEntries.Add(entry.IPAddress, 1);
            }
        }
        public Dictionary<IPAddress, uint> GetLogs()
        {
            if (_logEntries!.Count == 0)
                throw new NullReferenceException("HashSet is empty. More than one record was not found");
            return _logEntries;
        }

    }
}

