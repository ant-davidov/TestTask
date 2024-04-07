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
    public class IPAndMaskValidatorTests
    {
        [Fact]
        public void Validate_WithValidIpAndMask_ShouldNotThrowException()
        {    
            var args = new Arguments
            {
                AddressStart = "192.168.1.1",
                AddressMask = 24
            };
            var args2 = new Arguments
            {
                AddressStart = ""
            };
            var args3 = new Arguments();
           
            var iValidatorList = new List<IValidator> { new IPAndMaskValidator() };
            var validate = new ValidationArgs(iValidatorList);

            Assert.IsNotType<Exception>(() => validate.Validate(args));
            Assert.IsNotType<Exception>(() => validate.Validate(args2));
            Assert.IsNotType<Exception>(() => validate.Validate(args3));
        }
        [Theory]
        [InlineData(null, 24)]  
        [InlineData("notanip", 24)] 
        [InlineData("192.168.1.1", 33)] 
        public void Validate_WithInvalidIpOrMask_ShouldThrowException(string ip, int? mask)
        {
            var args = new Arguments
            {
                AddressStart = ip,
                AddressMask = mask
            };
            var iValidatorList = new List<IValidator> { new IPAndMaskValidator() };
            var validate = new ValidationArgs(iValidatorList);

            Assert.ThrowsAny<Exception>(() => validate.Validate(args));
        }
    }
}
