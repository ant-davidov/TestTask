using TestTask.Application.Models;

namespace TestTask.Application.Interfaces
{
    internal interface IArgumentHandler
    {
        void Handle(string argument, ref Arguments parsedArgs, ref int currentIndex, string[] args);
    }
}
