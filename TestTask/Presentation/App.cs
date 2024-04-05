using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Application.Services;

namespace TestTask.Presentation
{

    internal class App 
    {
        private readonly IConfiguration _config;
        public App(IConfiguration config)
        {
            _config = config;
        }

        public async Task Run(string[] args, IServiceProvider provider)
        {
          
            try
            {
              
                var a = new Class1(provider);
                await a.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }

       
    }
}
