using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Application.Models.Validation;
using TestTask.Application.Models;
using TestTask.Application.Interfaces;

namespace TestTask.UnitTests.ValidationArg
{
    public class TimeValidatorTests
    {
        [Fact]
        public void Validate_WithValidDates_ShouldNotThrowException()
        {
            var args = new Arguments
            {
                StartTime = "01.01.2024",
                EndTime = "02.01.2024"
            };
            var iValidatorList = new List<IValidator> { new TimeValidator() };
            var validate = new ValidationArgs(iValidatorList);

            validate.Validate(args);
            Assert.IsNotType<Exception>(() => validate.Validate(args));
        }

        [Theory]
        [InlineData("01.01.2024", "31.12.2023")] // End date before start date
        [InlineData("01.01.2024", "01.01.2024")] // End date same as start date
        [InlineData("01.01.2024", "31.02.2024")] // Invalid end date format
        [InlineData("01.01.2024", "notadate")]   // Invalid end date format
        [InlineData("notadate", "01.01.2024")]   // Invalid start date format
        [InlineData("notadate", "notadate")]     // Invalid start and end date format
        public void Validate_WithInvalidDates_ShouldThrowException(string startTime, string endTime)
        {
            var args = new Arguments
            {
                StartTime = startTime,
                EndTime = endTime
            };
            var iValidatorList = new List<IValidator> { new TimeValidator() };
            var validate = new ValidationArgs(iValidatorList);

            Assert.Throws<ArgumentException>(() => validate.Validate(args));

        }
    }
}
