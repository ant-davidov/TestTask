using TestTask.Application.Interfaces;

namespace TestTask.Application.Models.Validation
{
    internal class OutputFilePathValidator : IValidator
    {
        public void Validate(Arguments args)
        {
            string filePath = args.OutputFilePath;
            if (string.IsNullOrEmpty(filePath))
                throw new Exception("Empty path for save");
            string directory = Path.GetDirectoryName(filePath);
            if (string.IsNullOrEmpty(directory) || !Directory.Exists(directory) || directory == "\\")
                throw new Exception($"There is no directory for saving the file - {filePath}");
            if (Directory.Exists(filePath))
                throw new Exception($"{filePath} is a folder");
        }
    }
}
