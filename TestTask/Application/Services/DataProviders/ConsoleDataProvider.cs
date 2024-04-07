using TestTask.Application.Handlers;
using TestTask.Application.Interfaces;
using TestTask.Application.Models;

namespace TestTask.Application.Services.DataProvider
{
    internal class ConsoleDataProvider : IDataProvider
    {
        private readonly string[] _args;
        private readonly Dictionary<string, IArgumentHandler> _argumentHandlers = new Dictionary<string, IArgumentHandler>
        {
            { "--file-log", new FileArgumentHandler() },
            { "--file-output", new FileOutputArgumentHandler() },
            { "--address-start", new AddressStartArgumentHandler() },
            { "--address-mask", new AddressMaskArgumentHandler() },
            { "--time-start", new StartTimeArgumentHandler() },
            { "--time-end", new EndTimeArgumentHandler() }
        };
        public ConsoleDataProvider(string[] args)
        {
            _args = args;
        }
        public Arguments GetData()
        {
            var parsedArgs = new Arguments();
            for (int i = 0; i < _args.Length; i++)
            {
                if (_argumentHandlers.TryGetValue(_args[i].ToLower(), out var handler))
                {
                    if (i + 1 >= _args.Length)
                        throw new IndexOutOfRangeException($"The key is written ({_args[i]}) but the value is not written");
                    handler.Handle(_args[i], ref parsedArgs, ref i, _args);
                }
                else
                    throw new ArgumentException($"Unknown argument: {_args[i]}");
            }
            return parsedArgs;
        }



    }
}



