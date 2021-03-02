//using NSubstitute;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using XeroChallenge.Application.Services;
//using XeroChallenge.Domain.Entities;
//using XeroChallenge.Domain.Repositories;
//using Xunit;
//using System.Linq;
//using XeroChallenge.Application.DTOs;
//using XeroChallenge.Application.Exceptions;

//namespace XeroChallenge.Application.UnitTesting.Services
//{
//    public class ProductServiceTests
//    {

//        [Fact]
//        public void GetAllProducts_ReturnListOfProductDtos()
//        {
//            //Prepare
//            var productRepository = Substitute.For<IProductRepository>();
//            var productService = new ProductService(productRepository);
//            var expectedProductsList = ReturnExpectedProductsList();
//            productRepository.GetAllByFilter(string.Empty).Returns(expectedProductsList);

//            //Act
//            var actualProductsList = productService.GetAllProductsByName(string.Empty);

//            //Assert
//            productRepository.Received(1).GetAllByFilter(string.Empty);
//            Assert.NotNull(actualProductsList);
//            Assert.Equal(2, actualProductsList.Count());

//            //Assert Values
//            Assert.All(actualProductsList, actualProduct =>
//            {
//                var expectedProduct = expectedProductsList.Single(x => x.Id == actualProduct.Id);

//                Assert.NotNull(expectedProduct);
//                Assert.Equal(expectedProduct.Id, actualProduct.Id);
//                Assert.Equal(expectedProduct.Name, actualProduct.Name);
//                Assert.Equal(expectedProduct.Description, actualProduct.Description);
//                Assert.Equal(expectedProduct.DeliveryPrice, actualProduct.DeliveryPrice);
//                Assert.Equal(expectedProduct.Price, actualProduct.Price);
//            });
//        }

//        [Fact]
//        public void GetProduct_ProductExists_ReturnProductDto()
//        {
//            //Prepare
//            var productRepository = Substitute.For<IProductRepository>();
//            var productService = new ProductService(productRepository);
//            var expectedProductsList = ReturnExpectedProductsList();
//            var firstExpectedProduct = expectedProductsList.First();
//            var productId = firstExpectedProduct.Id;
//            productRepository.Get(productId).Returns(firstExpectedProduct);

//            //Act
//            var actualProduct = productService.GetProduct(productId);

//            //Assert
//            productRepository.Received(1).Get(productId);
//            Assert.NotNull(actualProduct);

//            //Assert Values
//            Assert.Equal(firstExpectedProduct.Id, actualProduct.Id);
//            Assert.Equal(firstExpectedProduct.Name, actualProduct.Name);
//            Assert.Equal(firstExpectedProduct.Description, actualProduct.Description);
//            Assert.Equal(firstExpectedProduct.DeliveryPrice, actualProduct.DeliveryPrice);
//            Assert.Equal(firstExpectedProduct.Price, actualProduct.Price);
//        }

//        [Fact]
//        public void GetProduct_ProductDoesntExist_ThrowProductNotFoundException()
//        {
//            //Prepare
//            var productRepository = Substitute.For<IProductRepository>();
//            var productService = new ProductService(productRepository);
//            var productId = Guid.NewGuid();
//            productRepository.Get(productId).Returns((Product)null);

//            //Act and Assert
//            Assert.Throws<ProductNotFoundException>(() => productService.GetProduct(productId));
//            productRepository.Received(1).Get(productId);
//        }

//        [Fact]
//        public void DeleteProduct_CompleteWithoutExceptions()
//        {
//            //Prepare
//            var productToBeDeletedId = Guid.NewGuid();
//            var productRepository = Substitute.For<IProductRepository>();
//            var productService = new ProductService(productRepository);

//            //Act
//            productService.DeleteProduct(productToBeDeletedId);

//            //Assert
//            productRepository.Received(1).Delete(productToBeDeletedId);
//        }

//        [Fact]
//        public void SaveProduct_CompleteWithoutExceptions()
//        {
//            //Prepare
//            var productToBeSaved = new ProductDto
//            {
//                Id = Guid.NewGuid(),
//                Name = "New Product1",
//                Description = "New Product1",
//                Price = 300m,
//                DeliveryPrice = 330m
//            };

//            var productRepository = Substitute.For<IProductRepository>();
//            var productService = new ProductService(productRepository);

//            productRepository.Save(Arg.Any<Product>()).Returns(productToBeSaved.Id);

//            //Act
//            var savedProductId = productService.CreateProduct(productToBeSaved);

//            //Assert
//            productRepository.Received(1).Save(Arg.Any<Product>());
//            Assert.Equal(productToBeSaved.Id, savedProductId);
//        }

//        [Fact]
//        public void GetProductOptions_ProductExists_ReturnListOfProductOptionsDto()
//        {
//            //Prepare
//            var productRepository = Substitute.For<IProductRepository>();
//            var productService = new ProductService(productRepository);
//            var expectedProductsList = ReturnExpectedProductsList();
//            var firstExpectedProduct = expectedProductsList.First();
//            var productId = firstExpectedProduct.Id;
//            productRepository.Get(productId).Returns(firstExpectedProduct);

//            //Act
//            var actualProductOptionsList = productService.GetProductOptions(productId);

//            //Assert
//            productRepository.Received(1).Get(productId);
//            Assert.NotNull(actualProductOptionsList);
//            Assert.Equal(3, actualProductOptionsList.Count());

//            Assert.All(actualProductOptionsList, actualProductOption =>
//            {
//                var expectedProductOption = firstExpectedProduct.Options.Single(x => x.Id == actualProductOption.Id);

//                Assert.NotNull(expectedProductOption);
//                Assert.Equal(expectedProductOption.Name, actualProductOption.Name);
//                Assert.Equal(expectedProductOption.Description, actualProductOption.Description);
//                Assert.Equal(productId, actualProductOption.ProductId);
//            });
//        }

//        [Fact]
//        public void GetProductOptions_ProductDoesntExist_ThrowProductNotFoundException()
//        {
//            //Prepare
//            var productRepository = Substitute.For<IProductRepository>();
//            var productService = new ProductService(productRepository);
//            var productId = Guid.NewGuid();
//            productRepository.Get(productId).Returns((Product)null);

//            //Act and Assert
//            Assert.Throws<ProductNotFoundException>(() => productService.GetProductOptions(productId));
//            productRepository.Received(1).Get(productId);
//        }

//        [Fact]
//        public void GetProductOption_ProductAndProductOptionExist_ReturnProductOptionDto()
//        {
//            //Prepare
//            var productRepository = Substitute.For<IProductRepository>();
//            var productService = new ProductService(productRepository);
//            var expectedProductsList = ReturnExpectedProductsList();
//            var firstExpectedProduct = expectedProductsList.First();
//            var productId = firstExpectedProduct.Id;
//            var expectedProductOption = firstExpectedProduct.Options.First();
//            var productOptionId = expectedProductOption.Id;

//            productRepository.Get(productId).Returns(firstExpectedProduct);

//            //Act
//            var actualProductOption = productService.GetProductOption(productId, productOptionId);

//            //Assert
//            Assert.NotNull(actualProductOption);
//            Assert.Equal(expectedProductOption.Id, actualProductOption.Id);
//            Assert.Equal(expectedProductOption.Name, actualProductOption.Name);
//            Assert.Equal(expectedProductOption.Description, actualProductOption.Description);
//            Assert.Equal(expectedProductOption.Product.Id, actualProductOption.ProductId);

//            productRepository.Received(1).Get(productId);
//        }

//        [Fact]
//        public void GetProductOption_ProductDoesntExist_ThrowProductNotFoundException()
//        {
//            //Prepare
//            var productRepository = Substitute.For<IProductRepository>();
//            var productService = new ProductService(productRepository);
//            var productId = Guid.NewGuid();
//            var productOptionId = Guid.NewGuid();

//            productRepository.Get(productId).Returns((Product)null);

//            //Act and Assert
//            Assert.Throws<ProductNotFoundException>(() => productService.GetProductOption(productId, productOptionId));
//            productRepository.Received(1).Get(productId);
//        }

//        [Fact]
//        public void GetProductOption_ProductOptionDoesntExist_ThrowProductOptionNotFoundException()
//        {
//            //Prepare
//            var productRepository = Substitute.For<IProductRepository>();
//            var productService = new ProductService(productRepository);
//            var expectedProductsList = ReturnExpectedProductsList();
//            var firstExpectedProduct = expectedProductsList.First();
//            var productId = firstExpectedProduct.Id;
//            var productOptionId = Guid.NewGuid();

//            productRepository.Get(productId).Returns(firstExpectedProduct);

//            //Act and Assert
//            Assert.Throws<ProductOptionNotFoundException>(() => productService.GetProductOption(productId, productOptionId));
//            productRepository.Received(1).Get(productId);
//        }

//        [Fact]
//        public void DeleteProductOption_ProductOptionDoesntExist_ThrowProductOptionNotFoundException()
//        {
//            //Prepare
//            var productRepository = Substitute.For<IProductRepository>();
//            var productService = new ProductService(productRepository);
//            var expectedProductsList = ReturnExpectedProductsList();
//            var firstExpectedProduct = expectedProductsList.First();
//            var productId = firstExpectedProduct.Id;
//            var productOptionId = firstExpectedProduct.Options.First().Id;

//            productRepository.Get(productId).Returns(firstExpectedProduct);

//            //Act
//            productService.DeleteProductOption(productId, productOptionId);

//            //Assert
//            productRepository.Received(1).Get(productId);
//            productRepository.Received(1).Save(firstExpectedProduct);
//            Assert.Equal(2, firstExpectedProduct.Options.Count);
//        }

//        [Fact]
//        public void SaveProductOption_ProductDoesntExist_AddNewProductOption()
//        {
//            //Prepare
//            var productRepository = Substitute.For<IProductRepository>();
//            var productService = new ProductService(productRepository);

//            //Act
//            productService.SaveProductOption(null);
//        }

//        private List<Product> ReturnExpectedProductsList()
//        {
//            var productsList = new List<Product>()
//            {
//                new Product
//                {
//                    Id = Guid.NewGuid(),
//                    Name = "Testing Name1",
//                    Description = "Testing Description1",
//                    Price= 100m,
//                    DeliveryPrice = 110m,
//                    Options = new List<ProductOption>()
//                    {
//                        new ProductOption
//                        {
//                            Id = Guid.NewGuid(),
//                            Name = "Option Name 1",
//                            Description = "Option Description 1"
//                        },
//                        new ProductOption
//                        {
//                            Id = Guid.NewGuid(),
//                            Name = "Option Name 2",
//                            Description = "Option Description 2"
//                        },
//                        new ProductOption
//                        {
//                            Id = Guid.NewGuid(),
//                            Name = "Option Name 3",
//                            Description = "Option Description 3"
//                        }
//                    }
//                },
//                 new Product
//                {
//                    Id = Guid.NewGuid(),
//                    Name = "Testing Name2",
//                    Description = "Testing Description2",
//                    Price= 200m,
//                    DeliveryPrice = 220m
//                }
//           };

//            var firstProduct = productsList.First();
//            firstProduct.Options.ForEach(p => p.Product = firstProduct);
//            return productsList;
//        }
//    }
//}
