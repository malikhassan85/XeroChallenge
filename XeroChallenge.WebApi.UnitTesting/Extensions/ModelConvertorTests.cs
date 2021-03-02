using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XeroChallenge.Domain.Entities;
using XeroChallenge.WebApi.Extensions;
using XeroChallenge.WebApi.Models;
using Xunit;

namespace XeroChallenge.WebApi.UnitTesting.Extensions
{
    public class ModelConvertorTests
    {
        [Fact]
        public void Can_MapFromProduct_ToProductDto()
        {
            //Prepare
            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Product 1",
                Description = "Product Description 1",
                Price = 100,
                DeliveryPrice = 10
            };

            //Act
            var productDto = product.ToDto();

            //Assert
            Assert.Equal(product.Id, productDto.Id);
            Assert.Equal(product.Name, productDto.Name);
            Assert.Equal(product.Description, productDto.Description);
            Assert.Equal(product.Price, productDto.Price);
            Assert.Equal(product.DeliveryPrice, productDto.DeliveryPrice);
        }

        [Fact]
        public void Can_MapFromProductDto_ToProduct()
        {
            //Prepare
            var productDto = new ProductDto()
            {
                Id = Guid.NewGuid(),
                Name = "Product 1",
                Description = "Product Description 1",
                Price = 100,
                DeliveryPrice = 10
            };

            //Act
            var product = productDto.ToDomainEntity();

            //Assert
            Assert.Equal(productDto.Id, product.Id);
            Assert.Equal(productDto.Name, product.Name);
            Assert.Equal(productDto.Description, product.Description);
            Assert.Equal(productDto.Price, product.Price);
            Assert.Equal(productDto.DeliveryPrice, product.DeliveryPrice);
        }

        [Fact]
        public void Can_MapFromProductOption_ToProductOptionDto()
        {
            //Prepare
            var productOption = new ProductOption()
            {
                Id = Guid.NewGuid(),
                Name = "Product Option 1",
                Description = "Product Option Description 1"
            };

            //Act
            var productOptionDto = productOption.ToDto();

            //Assert
            Assert.Equal(productOption.Id, productOptionDto.Id);
            Assert.Equal(productOption.Name, productOptionDto.Name);
            Assert.Equal(productOption.Description, productOptionDto.Description);
        }

        [Fact]
        public void Can_MapFromProductOptionDto_ToProductOption()
        {
            //Prepare
            var productOptionDto = new ProductOptionDto()
            {
                Id = Guid.NewGuid(),
                Name = "Product Option 1",
                Description = "Product Option Description 1"
            };

            //Act
            var productOption = productOptionDto.ToDomainEntity();

            //Assert
            Assert.Equal(productOptionDto.Id, productOption.Id);
            Assert.Equal(productOptionDto.Name, productOption.Name);
            Assert.Equal(productOptionDto.Description, productOption.Description);
        }

        [Fact]
        public void Can_MapFromProductOptionList_ToProductOptionDtoList()
        {
            //Prepare
            var productOptions = new List<ProductOption>()
            {
                new ProductOption()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product Option 1",
                    Description = "Product Option Description 1"
                },
                 new ProductOption()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product Option 2",
                    Description = "Product Option Description 2"
                },
             };

            //Act
            var productOptionDtos = productOptions.ToDto();

            //Assert
            Assert.NotNull(productOptionDtos);
            Assert.Equal(2, productOptionDtos.Count());

            Assert.All(productOptionDtos, optionDto =>
            {
                var productOption = productOptions.SingleOrDefault(p => p.Id == optionDto.Id);
                Assert.NotNull(productOption);
                Assert.Equal(productOption.Name, optionDto.Name);
                Assert.Equal(productOption.Description, optionDto.Description);
            });
        }

        [Fact]
        public void Can_MapFromProductList_ToProductDtoList()
        {
            //Prepare
            var products = new List<Product>()
            {
               new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 1",
                    Description = "Product Description 1",
                    Price = 100,
                    DeliveryPrice = 10
                },
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 2",
                    Description = "Product Description 2",
                    Price = 200,
                    DeliveryPrice = 220
                }
             };

            //Act
            var productDtos = products.ToDto();

            //Assert
            Assert.NotNull(productDtos);
            Assert.Equal(2, productDtos.Count());

            Assert.All(productDtos, productDto =>
            {
                var product = products.SingleOrDefault(p => p.Id == productDto.Id);
                Assert.NotNull(product);
                Assert.Equal(product.Name, productDto.Name);
                Assert.Equal(product.Description, productDto.Description);
                Assert.Equal(product.Price, productDto.Price);
                Assert.Equal(product.DeliveryPrice, productDto.DeliveryPrice);
            });
        }
    }
}
