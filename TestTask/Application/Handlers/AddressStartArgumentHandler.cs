using TestTask.Application.Interfaces;
using TestTask.Application.Models;

namespace TestTask.Application.Handlers
{
    internal class AddressStartArgumentHandler : IArgumentHandler
    {
        public void Handle(string argument, ref Arguments parsedArgs, ref int currentIndex, string[] args)
        {

            parsedArgs.AddressStart = args[++currentIndex];
        }
    }
}
