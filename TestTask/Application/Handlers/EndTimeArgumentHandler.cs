using TestTask.Application.Interfaces;
using TestTask.Application.Models;

namespace TestTask.Application.Handlers
{
    internal class EndTimeArgumentHandler : IArgumentHandler
    {
        public void Handle(string argument, ref Arguments parsedArgs, ref int currentIndex, string[] args)
        {
            var time = args[++currentIndex];
            if (String.IsNullOrEmpty(time))
                throw new ArgumentException("Empty string with end time");
            parsedArgs.EndTime = time;
        }
    }
}
