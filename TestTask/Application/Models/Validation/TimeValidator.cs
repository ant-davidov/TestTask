using System.Globalization;
using TestTask.Application.Interfaces;

namespace TestTask.Application.Models.Validation
{
    internal class TimeValidator : IValidator
    {
        public void Validate(Arguments args)
        {
            string startTime = args.StartTime;
            string endTime = args.EndTime;
            if (DateTime.TryParseExact(startTime, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var startDate) && DateTime.TryParseExact(endTime, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var endDate))
            {
                if (endDate <= startDate)
                    throw new ArgumentException("The end date is less than the start date or same");
                return;
            }
            throw new ArgumentException("Invalid date format. try --time-start and --time-end dd.MM.yyyy");
        }
    }
}
