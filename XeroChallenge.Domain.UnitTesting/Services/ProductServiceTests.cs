using NSubstitute;
using System;
using System.Collections.Generic;
using XeroChallenge.Domain.Entities;
using XeroChallenge.Domain.Repositories;
using Xunit;
using System.Linq;
using XeroChallenge.Domain.Services;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using XeroChallenge.Domain.Exceptions;

namespace XeroChallenge.Application.UnitTesting.Services
{
    public class ProductServiceTests
    {

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task GetAllProducts_NameIsNullOrEmpty_ReturnListOfProducts(string name)
        {
            //Prepare
            var logger = Substitute.For<ILogger<ProductService>>();
            var productRepository = Substitute.For<IProductRepository>();
            var productService = new ProductService(productRepository, logger);
            var expectedProductsList = ReturnExpectedProductsList();
            productRepository.GetAll().Returns(expectedProductsList);

            //Act
            var actualProductsList = await productService.GetAllProductsByName(name);

            //Assert
            await productRepository.Received(1).GetAll();
            Assert.NotNull(actualProductsList);
            Assert.Equal(2, actualProductsList.ToList().Count());

            //Assert Values
            Assert.All(actualProductsList, actualProduct =>
            {
                var expectedProduct = expectedProductsList.Single(x => x.Id == actualProduct.Id);

                Assert.NotNull(expectedProduct);
                Assert.Equal(expectedProduct.Id, actualProduct.Id);
                Assert.Equal(expectedProduct.Name, actualProduct.Name);
                Assert.Equal(expectedProduct.Description, actualProduct.Description);
                Assert.Equal(expectedProduct.DeliveryPrice, actualProduct.DeliveryPrice);
                Assert.Equal(expectedProduct.Price, actualProduct.Price);
            });
        }


        [Fact]
        public async Task GetAllProducts_NameIsNotNullOrEmpty_ReturnListOfProducts()
        {
            //Prepare
            var logger = Substitute.For<ILogger<ProductService>>();
            var productRepository = Substitute.For<IProductRepository>();
            var productService = new ProductService(productRepository, logger);
            var expectedProductsList = ReturnExpectedProductsList();
            var firstExpectedProduct = expectedProductsList.First();
            var productName = firstExpectedProduct.Name;

            productRepository.GetAllByName(productName).Returns(new List<Product>() { firstExpectedProduct });

            //Act
            var actualProductsList = await productService.GetAllProductsByName(productName);

            //Assert
            await productRepository.Received(1).GetAllByName(productName);
            Assert.NotNull(actualProductsList);
            Assert.Single(actualProductsList);

            //Assert Values
            Assert.All(actualProductsList, actualProduct =>
            {
                var expectedProduct = expectedProductsList.Single(x => x.Id == actualProduct.Id);

                Assert.NotNull(expectedProduct);
                Assert.Equal(expectedProduct.Id, actualProduct.Id);
                Assert.Equal(expectedProduct.Name, actualProduct.Name);
                Assert.Equal(expectedProduct.Description, actualProduct.Description);
                Assert.Equal(expectedProduct.DeliveryPrice, actualProduct.DeliveryPrice);
                Assert.Equal(expectedProduct.Price, actualProduct.Price);
            });
        }

        [Fact]
        public async Task GetProduct_ProductExists_ReturnProduct()
        {
            //Prepare
            var logger = Substitute.For<ILogger<ProductService>>();
            var productRepository = Substitute.For<IProductRepository>();
            var productService = new ProductService(productRepository, logger);
            var expectedProductsList = ReturnExpectedProductsList();
            var firstExpectedProduct = expectedProductsList.First();
            var productId = firstExpectedProduct.Id;
            productRepository.Get(productId).Returns(firstExpectedProduct);

            //Act
            var actualProduct = await productService.GetProduct(productId);

            //Assert
            await productRepository.Received(1).Get(productId);
            Assert.NotNull(actualProduct);

            //Assert Values
            Assert.Equal(firstExpectedProduct.Id, actualProduct.Id);
            Assert.Equal(firstExpectedProduct.Name, actualProduct.Name);
            Assert.Equal(firstExpectedProduct.Description, actualProduct.Description);
            Assert.Equal(firstExpectedProduct.DeliveryPrice, actualProduct.DeliveryPrice);
            Assert.Equal(firstExpectedProduct.Price, actualProduct.Price);
        }

        [Fact]
        public async Task GetProduct_ProductDoesntExist_ThrowProductNotFoundException()
        {
            //Prepare
            var productRepository = Substitute.For<IProductRepository>();
            var logger = Substitute.For<ILogger<ProductService>>();
            var productService = new ProductService(productRepository, logger);
            var productId = Guid.NewGuid();
            productRepository.Get(productId).Returns((Product)null);

            //Act and Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(() => productService.GetProduct(productId));
            await productRepository.Received(1).Get(productId);
        }

        [Fact]
        public async Task CreateProduct_ProductParameterIsNull_ThrowArgumentNullException()
        {
            //Prepare
            var productRepository = Substitute.For<IProductRepository>();
            var logger = Substitute.For<ILogger<ProductService>>();
            var productService = new ProductService(productRepository, logger);

            //Act and Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => productService.CreateProduct(null));
        }

        [Fact]
        public async Task CreateProduct_ProductIdIsNotEmpty_ThrowArgumentNullException()
        {
            //Prepare
            var productRepository = Substitute.For<IProductRepository>();
            var logger = Substitute.For<ILogger<ProductService>>();
            var productService = new ProductService(productRepository, logger);
            var product = new Product { Id = Guid.NewGuid() };
            //Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => productService.CreateProduct(product));
        }


        [Fact]
        public async Task CreateProduct_ProductIsValid_SaveProduct()
        {
            //Prepare
            var productRepository = Substitute.For<IProductRepository>();
            var logger = Substitute.For<ILogger<ProductService>>();
            var productService = new ProductService(productRepository, logger);
            var product = new Product { Id = Guid.Empty, Name = "Test Name", Description = "Test Description", Price = 100, DeliveryPrice = 10 };

            //Act
            await productService.CreateProduct(product);

            //Assert
            await productRepository.Received(1).Save(product);
        }

        [Fact]
        public async Task UpdateProduct_ProductParameterIsNull_ThrowArgumentNullException()
        {
            //Prepare
            var productRepository = Substitute.For<IProductRepository>();
            var logger = Substitute.For<ILogger<ProductService>>();
            var productService = new ProductService(productRepository, logger);

            //Act and Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => productService.UpdateProduct(null));
        }

        [Fact]
        public async Task UpdateProduct_ProductIdIsEmpty_ThrowArgumentNullException()
        {
            //Prepare
            var productRepository = Substitute.For<IProductRepository>();
            var logger = Substitute.For<ILogger<ProductService>>();
            var productService = new ProductService(productRepository, logger);
            var product = new Product { Id = Guid.Empty };
            //Act and Assert
            await Assert.ThrowsAsync<ArgumentException>(() => productService.UpdateProduct(product));
        }


        [Fact]
        public async Task UpdateProduct_ProductIsValid_SaveProduct()
        {
            //Prepare
            var productRepository = Substitute.For<IProductRepository>();
            var logger = Substitute.For<ILogger<ProductService>>();
            var productService = new ProductService(productRepository, logger);
            var product = new Product { Id = Guid.NewGuid(), Name = "Test Name", Description = "Test Description", Price = 100, DeliveryPrice = 10 };

            //Act
            await productService.UpdateProduct(product);

            //Assert
            await productRepository.Received(1).Save(product);
        }

        [Fact]
        public async Task DeleteProduct_CompleteWithoutExceptions()
        {
            //Prepare
            var productToBeDeletedId = Guid.NewGuid();
            var productRepository = Substitute.For<IProductRepository>();
            var logger = Substitute.For<ILogger<ProductService>>();
            var productService = new ProductService(productRepository, logger);

            //Act
            await productService.DeleteProduct(productToBeDeletedId);

            //Assert
            await productRepository.Received(1).Delete(productToBeDeletedId);
        }

        [Fact]
        public async Task GetProductOptions_ProductExists_ReturnListOfProductOptions()
        {
            //Prepare
            var productRepository = Substitute.For<IProductRepository>();
            var logger = Substitute.For<ILogger<ProductService>>();
            var productService = new ProductService(productRepository, logger);
            var expectedProductsList = ReturnExpectedProductsList();
            var firstExpectedProduct = expectedProductsList.First();
            var productId = firstExpectedProduct.Id;
            productRepository.Get(productId).Returns(firstExpectedProduct);

            //Act
            var actualProductOptionsList = await productService.GetProductOptions(productId);

            //Assert
            await productRepository.Received(1).Get(productId);
            Assert.NotNull(actualProductOptionsList);
            Assert.Equal(3, actualProductOptionsList.Count());

            Assert.All(actualProductOptionsList, actualProductOption =>
            {
                var expectedProductOption = firstExpectedProduct.Options.Single(x => x.Id == actualProductOption.Id);

                Assert.NotNull(expectedProductOption);
                Assert.Equal(expectedProductOption.Name, actualProductOption.Name);
                Assert.Equal(expectedProductOption.Description, actualProductOption.Description);
            });
        }

        [Fact]
        public async Task GetProductOptions_ProductDoesntExist_ThrowProductNotFoundException()
        {
            //Prepare
            var productRepository = Substitute.For<IProductRepository>();
            var logger = Substitute.For<ILogger<ProductService>>();
            var productService = new ProductService(productRepository, logger);
            var productId = Guid.NewGuid();
            productRepository.Get(productId).Returns((Product)null);

            //Act and Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(() => productService.GetProductOptions(productId));
            await productRepository.Received(1).Get(productId);
        }

        [Fact]
        public async Task GetProductOption_ProductAndProductOptionExist_ReturnProductOptionDto()
        {
            //Prepare
            var productRepository = Substitute.For<IProductRepository>();

            var logger = Substitute.For<ILogger<ProductService>>();
            var productService = new ProductService(productRepository, logger);
            var expectedProductsList = ReturnExpectedProductsList();
            var firstExpectedProduct = expectedProductsList.First();
            var productId = firstExpectedProduct.Id;
            var expectedProductOption = firstExpectedProduct.Options.First();
            var productOptionId = expectedProductOption.Id;

            productRepository.Get(productId).Returns(firstExpectedProduct);

            //Act
            var actualProductOption = await productService.GetProductOption(productId, productOptionId);

            //Assert
            Assert.NotNull(actualProductOption);
            Assert.Equal(expectedProductOption.Id, actualProductOption.Id);
            Assert.Equal(expectedProductOption.Name, actualProductOption.Name);
            Assert.Equal(expectedProductOption.Description, actualProductOption.Description);

            await productRepository.Received(1).Get(productId);
        }

        [Fact]
        public async Task GetProductOption_ProductDoesntExist_ThrowProductNotFoundException()
        {
            //Prepare
            var productRepository = Substitute.For<IProductRepository>();
            var logger = Substitute.For<ILogger<ProductService>>();
            var productService = new ProductService(productRepository, logger);
            var productId = Guid.NewGuid();
            var productOptionId = Guid.NewGuid();

            productRepository.Get(productId).Returns((Product)null);

            //Act and Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(() => productService.GetProductOption(productId, productOptionId));
            await productRepository.Received(1).Get(productId);
        }

        [Fact]
        public async Task GetProductOption_ProductOptionDoesntExist_ThrowProductOptionNotFoundException()
        {
            //Prepare
            var productRepository = Substitute.For<IProductRepository>();
            var logger = Substitute.For<ILogger<ProductService>>();
            var productService = new ProductService(productRepository, logger);
            var expectedProductsList = ReturnExpectedProductsList();
            var firstExpectedProduct = expectedProductsList.First();
            var productId = firstExpectedProduct.Id;
            var productOptionId = Guid.NewGuid();

            productRepository.Get(productId).Returns(firstExpectedProduct);

            //Act and Assert
            await Assert.ThrowsAsync<ProductOptionNotFoundException>(() => productService.GetProductOption(productId, productOptionId));
            await productRepository.Received(1).Get(productId);
        }

        [Fact]
        public async Task DeleteProductOption_ProductExists_CompleteWithoutExceptions()
        {
            //Prepare
            var productRepository = Substitute.For<IProductRepository>();
            var logger = Substitute.For<ILogger<ProductService>>();
            var productService = new ProductService(productRepository, logger);
            var expectedProductsList = ReturnExpectedProductsList();
            var firstExpectedProduct = expectedProductsList.First();
            var productId = firstExpectedProduct.Id;
            var productOptionId = firstExpectedProduct.Options.First().Id;

            productRepository.Get(productId).Returns(firstExpectedProduct);

            //Act
            await productService.DeleteProductOption(productId, productOptionId);

            //Assert
            await productRepository.Received(1).Get(productId);
            await productRepository.Received(1).Save(firstExpectedProduct);
            Assert.Equal(2, firstExpectedProduct.Options.Count);
        }


        [Fact]
        public async Task DeleteProductOption_ProductDoesntExist_ThrowProductNotFoundException()
        {
            //Prepare
            var productRepository = Substitute.For<IProductRepository>();
            var logger = Substitute.For<ILogger<ProductService>>();
            var productService = new ProductService(productRepository, logger);
            var productId = Guid.NewGuid();
            var productOptionId = Guid.NewGuid();

            productRepository.Get(productId).Returns((Product)null);

            //Act and Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(() => productService.DeleteProductOption(productId, productOptionId));
            await productRepository.Received(1).Get(productId);
        }

        [Fact]
        public async Task CreateProductOption_ProductExists_AddNewProductOption()
        {
            //Prepare
            var productRepository = Substitute.For<IProductRepository>();
            var expectedProductsList = ReturnExpectedProductsList();

            var logger = Substitute.For<ILogger<ProductService>>();
            var productService = new ProductService(productRepository, logger);
            var firstExpectedProduct = expectedProductsList.First();
            var productId = firstExpectedProduct.Id;
            var productOption = new ProductOption();
            productRepository.Get(productId).Returns(firstExpectedProduct);

            //Act
            await productService.CreateProductOption(productId, productOption);

            //Assert
            await productRepository.Received(1).Get(productId);
            await productRepository.Received(1).Save(firstExpectedProduct);
            Assert.Equal(4, firstExpectedProduct.Options.Count);
        }

        private List<Product> ReturnExpectedProductsList()
        {
            var productsList = new List<Product>()
                    {
                        new Product
                        {
                            Id = Guid.NewGuid(),
                            Name = "Testing Name1",
                            Description = "Testing Description1",
                            Price= 100m,
                            DeliveryPrice = 110m,
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
                                },
                                new ProductOption
                                {
                                    Id = Guid.NewGuid(),
                                    Name = "Option Name 3",
                                    Description = "Option Description 3"
                                }
                            }
                        },
                         new Product
                        {
                            Id = Guid.NewGuid(),
                            Name = "Testing Name2",
                            Description = "Testing Description2",
                            Price= 200m,
                            DeliveryPrice = 220m
                        }
                   };

            var firstProduct = productsList.First();
            firstProduct.Options.ForEach(p => p.Product = firstProduct);
            return productsList;
        }
    }
}
