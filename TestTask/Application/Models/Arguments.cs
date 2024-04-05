using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Application.Models
{
    internal class Arguments
    {
        public string FilePath { get; set; } = null!;
        public string OutputFilePath { get;  set; } = null!;
        public string? AddressStart { get; set; }
        public int? AddressMask { get; set; }
    }
}
