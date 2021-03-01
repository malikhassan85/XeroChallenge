using System;
using System.Collections.Generic;
using System.Text;
using XeroChallenge.Application.Exceptions;
using Xunit;

namespace XeroChallenge.Application.UnitTesting.Exceptions
{
    public class ProductOptionNotFoundExceptionTests
    {
        [Fact]
        public void CreateException_ProductOptionIdIsProvided_MessageIsFormattedCorrectly()
        {
            var testingGuidValue = new Guid();

            var exception = new ProductOptionNotFoundException(testingGuidValue);

            Assert.Contains(testingGuidValue.ToString(), exception.Message);
        }
    }
}
