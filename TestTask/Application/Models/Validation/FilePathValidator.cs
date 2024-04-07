using TestTask.Application.Interfaces;

namespace TestTask.Application.Models.Validation
{
    internal class FilePathValidator : IValidator
    {
        public void Validate(Arguments args)
        {
            string filePath = args.FilePath;
            if (string.IsNullOrEmpty(filePath))
                throw new Exception("Path is null");
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Not found {filePath}");
        }
    }
}
