using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XeroChallenge.Domain.Entities;
using Xunit;

namespace XeroChallenge.Domain.UnitTesting.Entities
{
    public class ProductTests
    {
        [Fact]
        public void RemoveProductOption_OptionsHasTheItem_RemoveItem()
        {
            //Prepare
            var product = GetTestingProduct();
            var productOptionId = product.Options.First().Id;

            //Act 
            product.RemoveProductOption(productOptionId);

            //Assert
            Assert.NotNull(product.Options);
            Assert.Single(product.Options);
        }

        [Fact]
        public void RemoveProductOption_OptionsDoesntHaveTheItem_NothingIsRemoved()
        {
            //Prepare
            var product = GetTestingProduct();
            var productOptionId = Guid.NewGuid();

            //Act 
            product.RemoveProductOption(productOptionId);

            //Assert
            Assert.NotNull(product.Options);
            Assert.Equal(2, product.Options.Count);
        }

        [Fact]
        public void AddProductOption_ProductHasOptions_NewItemIsAdded()
        {
            //Prepare
            var product = GetTestingProduct();
            var productOption = new ProductOption
            {
                Name = "Third Option",
                Description = "Third Option Description"
            };

            //Act 
            product.AddProductOption(productOption);

            //Assert
            Assert.NotNull(product.Options);
            Assert.Equal(3, product.Options.Count);
        }


        [Fact]
        public void AddProductOption_ProductHasNoOptions_NewItemIsAdded()
        {
            //Prepare
            var product = GetTestingProduct();
            product.Options = null;
            var productOption = new ProductOption
            {
                Name = "Third Option",
                Description = "Third Option Description"
            };

            //Act 
            product.AddProductOption(productOption);

            //Assert
            Assert.NotNull(product.Options);
            Assert.Single(product.Options);
        }

        private Product GetTestingProduct()
        {
            return new Product
            {
                Id = Guid.NewGuid(),
                Options = new List<ProductOption>()
                {
                    new ProductOption
                    {
                        Id = Guid.NewGuid(),
                        Name = "Option Name 1",
                        Description = "Option Description 1"
                    },
                    new ProductOption
                    {
                        Id = Guid.NewGuid(),
                        Name = "Option Name 2",
                        Description = "Option Description 2"
                    }
                }
            };
        }
    }
}
