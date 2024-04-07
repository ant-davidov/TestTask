using TestTask.Application.Models;

namespace TestTask.Application.Interfaces
{
    internal interface IValidator
    {
        void Validate(Arguments args);
    }
}
