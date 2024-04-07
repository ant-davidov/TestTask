using System.Net;

namespace TestTask.Application.Interfaces
{
    internal interface IWrite
    {
        public void Write(string path, Dictionary<IPAddress, uint> repository);
    }
}
