using TestTask.Application.Interfaces;
using TestTask.Application.Models.Validation;
using TestTask.Application.Services.FileAccess;
using TestTask.Persistence;

namespace TestTask.Application.Services
{
    internal class Handler
    {

        private readonly Repository repository;
        public Handler()
        {
            repository = new Repository();
        }
        public void Start()
        {
            var dataProvider = ServiceHelper.GetService<IDataProvider>();
            var args = dataProvider!.GetData();
            var validator = new ValidationArgs();
            validator.Validate(args);
            var filter = new IpFilter(args);

            foreach (List<string> block in ReadDataFromFile.ReadBlocks(args.FilePath, 10000))
            {
                foreach (var line in block)
                {
                    var log = ParserLog.ParseLogLine(line);
                    if (log != null && filter.Filter(log))
                        repository.Add(log);
                }
            }
            var writeToFile = ServiceHelper.GetService<IWrite>();
            writeToFile.Write(args.OutputFilePath, repository.GetLogs());

        }
    }
}
