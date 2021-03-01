using System;
using System.Collections.Generic;
using System.Text;
using XeroChallenge.Application.Exceptions;
using Xunit;

namespace XeroChallenge.Application.UnitTesting.Exceptions
{
    public class ProductNotFoundExceptionTests
    {
        [Fact]
        public void CreateException_ProductIdIsProvided_MessageIsFormattedCorrectly()
        {
            var testingGuidValue = new Guid();

            var exception = new ProductNotFoundException(testingGuidValue);

            Assert.Contains(testingGuidValue.ToString(), exception.Message);
        }
    }
}
