
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TestTask.Application.Interfaces;
using TestTask.Persistence;

namespace TestTask.Application.Services
{
    internal class Class1
    {
        private readonly IServiceProvider _serviceProvide;
        public readonly Repository repository;
        public Class1(IServiceProvider service)
        {
            _serviceProvide = service;
            repository = new Repository();
        }
        public async Task Start()
        {
            var dataProvider = _serviceProvide.GetService<IDataProvider>();
            var args = dataProvider!.GetData();
            args.FilePath = "C:\\Users\\golden\\source\\repos\\ConsoleApp7\\ConsoleApp7\\bin\\Release\\net6.0\\ip_addresses.txt";
            Validation.validate(args);
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
         
           
                foreach (List<string> block in ReadBlocksAsync(args.FilePath, 10000))
                {
                        foreach (var line in block)
                        {
                            var a = ParseLogLine(line);
                            //collection.Insert(a);
                            repository.Add(a);
                        }

                        
                      
                    

                }
       
            using (StreamWriter writer = new StreamWriter("111.txt"))
            {
                foreach (var entry in repository.Get())
                {
                    writer.WriteLine($"{entry.IPAddress},{entry.MinTime},{entry.MaxTime},{entry.size}");
                }
            }

            stopWatch.Stop();
            await Console.Out.WriteLineAsync(stopWatch.Elapsed.ToString());
        }

        private LogEntry ParseLogLine(string line)
        {

            var splitLine = line.Split('\t');
            IPAddress ip;
            var isValidIp = IPAddress.TryParse(splitLine[0], out ip);
            if (!isValidIp)
            {
                Console.WriteLine($"Not Valid ip {splitLine[0]} skip line");
                return null;
            }
            DateTime dateTime;
            if (DateTime.TryParse(splitLine[1], out dateTime))
                return new LogEntry { IPAddress = ip.ToString(), MaxTime = dateTime, MinTime = dateTime };
            Console.WriteLine($"Not Valid datetime {splitLine[1]} skip line, ip - {splitLine[0]}");
            return null;

        }

        private void IncrementRequestCount(string iPAddress)
        {

        }



        static IEnumerable<List<string>> ReadBlocksAsync(string filePath, int blockSize)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                List<string> block = new List<string>();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    block.Add(line);

                    if (block.Count >= blockSize)
                    {
                        yield return block;
                        block = new List<string>();
                    }
                }
                if (block.Count > 0)
                {
                    yield return block;
                }
            }
        }





    }
}
