using System.Net;
using TestTask.Application.Interfaces;

namespace TestTask.Application.Services.FileAccess
{
    internal class WriteToFile : IWrite
    {
        public void Write(string path, Dictionary<IPAddress, uint> logs)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    foreach (var entry in logs)
                    {
                        writer.WriteLine($"{entry.Key} : {entry.Value}");
                    }
                }
            }
            catch
            {
                throw new Exception($"Error write to file {path}");
            }

        }
    }
}
