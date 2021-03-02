using System;
using Xunit;

namespace XeroChallenge.Domain.Entities
{
    public class ProductOptionTests
    {
        [Fact]
        public void UpdateValues()
        {
            //Prepare
            var productOption = new ProductOption();
            var newProductOption = new ProductOption()
            {
                Id = Guid.NewGuid(),
                Name = "Test Product Option Name",
                Description = "Test Product Option Description",
            };

            //Act
            productOption.UpdateValues(newProductOption);

            //Assert
            Assert.Equal(Guid.Empty, productOption.Id);
            Assert.Equal(newProductOption.Name, productOption.Name);
            Assert.Equal(newProductOption.Description, productOption.Description);
        }
    }
}
