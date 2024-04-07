namespace TestTask.Application.Models
{
    internal class Arguments
    {
        public string FilePath { get; set; } = null!;
        public string OutputFilePath { get; set; } = null!;
        public string AddressStart { get; set; } = string.Empty;
        public int? AddressMask { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
    }
}
