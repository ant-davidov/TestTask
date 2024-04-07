using Microsoft.Extensions.Configuration;
using TestTask.Application.Interfaces;
using TestTask.Application.Models;

namespace TestTask.Application.Services.DataProvider
{
    internal class JsonDataProvider : IDataProvider
    {
        private readonly IConfiguration _configuration;
        public JsonDataProvider(IConfiguration config)
        {
            _configuration = config;
        }
        public Arguments GetData()
        {
            Arguments args = new Arguments();
            _configuration.GetSection("Arguments").Bind(args);
            if (args == null)
                throw new Exception("Args or file not found");
            return args;
        }
    }
}
