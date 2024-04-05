using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return new Arguments();
        }
    }
}
