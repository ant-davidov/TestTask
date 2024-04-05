using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Persistence
{
    //    internal class Repository
    //    {

    //        private SQLiteConnection _connection;
    //        public Repository()
    //        {
    //            _connection = ServiceHelper.GetService<SQLiteConnection>();
    //        }

    //        public void Add(LogEntry entry)
    //        {
    //            try
    //            {
    //                string insertQuery = @"
    //            INSERT INTO IPTable (IPAddress, TimeMax, TimeMin, Count)
    //            VALUES (@IPAddress, @TimeMax, @TimeMin, @Count);";

    //                SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, _connection);
    //                insertCommand.Parameters.AddWithValue("@IPAddress", entry.IPAddress);
    //                insertCommand.Parameters.AddWithValue("@TimeMax", entry.MaxTime);
    //                insertCommand.Parameters.AddWithValue("@TimeMin", entry.MinTime);
    //                insertCommand.Parameters.AddWithValue("@Count", entry.size);
    //                insertCommand.ExecuteNonQuery();
    //            }
    //            catch (SQLiteException ex)
    //            {

    //                    // Если запись уже существует, обновляем данные
    //                    string updateQuery = @"
    //                UPDATE IPTable 
    //                SET TimeMax = @TimeMax, 
    //                    TimeMin = @TimeMin, 
    //                    Count = Count + 1 
    //                WHERE IPAddress = @IPAddress;";

    //                    SQLiteCommand updateCommand = new SQLiteCommand(updateQuery, _connection);
    //                    updateCommand.Parameters.AddWithValue("@IPAddress", entry.IPAddress);
    //                    updateCommand.Parameters.AddWithValue("@TimeMax", entry.MaxTime);
    //                    updateCommand.Parameters.AddWithValue("@TimeMin", entry.MinTime);
    //                    updateCommand.ExecuteNonQuery();

    //            }
    //        }

    //    }
    //}




    //internal class Repository
    //{

    //    private Dictionary<IPAddress, LogEntry> _logEntries;
    //    public Repository()
    //    {
    //        _logEntries = new Dictionary<IPAddress, LogEntry>();
    //    }

    //    public void Add(LogEntry entry)
    //    {
    //        LogEntry tempEntry = null;
    //        _logEntries.TryGetValue(entry.IPAddress, out tempEntry);
    //        if (tempEntry == null)
    //        {
    //            entry.size += 1;
    //            _logEntries.Add(entry.IPAddress, entry);
    //        }
    //        else
    //        {
    //            if (tempEntry.MaxTime < entry.MaxTime)
    //                tempEntry.MaxTime = entry.MaxTime;
    //            if (tempEntry.MinTime > entry.MinTime)
    //                tempEntry.MinTime = entry.MinTime;
    //            tempEntry.size += 1;
    //            _logEntries[entry.IPAddress] = tempEntry;
    //        }
    //    }

    //}




    internal class Repository
    {

        private HashSet<LogEntry> _logEntries;
        public Repository()
        {
            _logEntries = new HashSet<LogEntry>();
        }

        public void Add(LogEntry entry)
        {
            LogEntry tempEntry = null;
            _logEntries.TryGetValue(entry, out tempEntry);
            if (tempEntry == null)
            {
                entry.size += 1;
                _logEntries.Add(entry);
            }
            else
            {
                _logEntries.Remove(tempEntry);
                if (tempEntry.MaxTime < entry.MaxTime)
                    tempEntry.MaxTime = entry.MaxTime;
                if (tempEntry.MinTime > entry.MinTime)
                    tempEntry.MinTime = entry.MinTime;
                tempEntry.size += 1;
                _logEntries.Add(tempEntry);

            }
        }
        public HashSet<LogEntry> Get ()
        {
            return _logEntries;
        }

    }
}









