using TestTask.Application.Interfaces;
using TestTask.Application.Models;

namespace TestTask.Application.Handlers
{
    internal class FileArgumentHandler : IArgumentHandler
    {
        public void Handle(string argument, ref Arguments parsedArgs, ref int currentIndex, string[] args)
        {

            var path = args[++currentIndex];
            if (String.IsNullOrEmpty(path))
                throw new ArgumentException("Empty path to open file log");
            parsedArgs.FilePath = path;
        }
    }
}
