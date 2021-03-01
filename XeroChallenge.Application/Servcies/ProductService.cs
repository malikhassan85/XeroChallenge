using System;
using System.Collections.Generic;
using System.Text;
using XeroChallenge.Application.DTOs;
using XeroChallenge.Application.Exceptions;
using XeroChallenge.Domain.Repositories;
using System.Linq;
using XeroChallenge.Domain.Entities;

namespace XeroChallenge.Application.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<ProductDto> GetAllProducts()
        {
            var productList = _productRepository.GetAll();

            return productList?.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                DeliveryPrice = p.DeliveryPrice
            });
        }

        public ProductDto GetProduct(Guid productId)
        {
            var product = GetProductFromRepository(productId);

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                DeliveryPrice = product.DeliveryPrice
            };
        }

        public void DeleteProduct(Guid productId)
        {
            _productRepository.Delete(productId);
        }


        public Guid SaveProduct(ProductDto product)
        {
            var entity = new Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                DeliveryPrice = product.DeliveryPrice
            };

            _productRepository.Save(entity);

            return entity.Id;
        }

        public ProductOptionDto GetProductOption(Guid productId, Guid optionId)
        {
            var product = GetProductFromRepository(productId);
            var productOption = product.Options.SingleOrDefault(p => p.Id == optionId);

            if (productOption == null)
                throw new ProductOptionNotFoundException(optionId);

            return new ProductOptionDto
            {
                Id = productOption.Id,
                Name = productOption.Name,
                Description = productOption.Description,
                ProductId = productOption.Product.Id
            };
        }

        public IEnumerable<ProductOptionDto> GetProductOptions(Guid productId)
        {
            var product = GetProductFromRepository(productId);

            return product.Options.Select(p => new ProductOptionDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                ProductId = productId
            });
        }


        public Guid SaveProductOption(ProductOptionDto productOption)
        {
            throw new NotImplementedException();
        }

        public void DeleteProductOption(Guid productId, Guid optionId)
        {
            var product = _productRepository.Get(productId);
            product.RemoveProductOption(optionId);
            _productRepository.Save(product);
        }

        private Product GetProductFromRepository(Guid Id)
        {
            var product = _productRepository.Get(Id);

            if (product == null)
                throw new ProductNotFoundException(Id);

            return product;
        }
    }
}
