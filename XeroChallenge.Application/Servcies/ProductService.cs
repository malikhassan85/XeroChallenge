using System;
using System.Collections.Generic;
using System.Text;
using XeroChallenge.Application.DTOs;
using XeroChallenge.Application.Exceptions;
using XeroChallenge.Domain.Repositories;
using System.Linq;
using XeroChallenge.Domain.Entities;
using EnsureThat;
using System.Threading.Tasks;

namespace XeroChallenge.Application.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsByName(string name)
        {
            var productList = await _productRepository.GetAllByFilter(name);

            return productList?.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                DeliveryPrice = p.DeliveryPrice
            });
        }

        public async Task<ProductDto> GetProduct(Guid productId)
        {
            var product = await GetProductFromRepository(productId);

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                DeliveryPrice = product.DeliveryPrice
            };
        }

        public async Task<Guid> CreateProduct(ProductDto productDto)
        {
            if (productDto == null)
                throw new ArgumentNullException(nameof(productDto));

            if (productDto.Id != Guid.Empty)
                throw new ArgumentException(nameof(productDto.Id), "Id should be empty when creating a new product");

            var entity = new Product()
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                DeliveryPrice = productDto.DeliveryPrice
            };

            await _productRepository.Save(entity);

            return entity.Id;
        }

        public async Task<Guid> UpdateProduct(ProductDto productDto)
        {
            if (productDto == null)
                throw new ArgumentNullException(nameof(productDto));

            if (productDto.Id == Guid.Empty)
                throw new ArgumentNullException(nameof(productDto.Id), "Id can't be empty when updating an existing product");

            var entity = new Product
            {
                Id = productDto.Id,
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                DeliveryPrice = productDto.DeliveryPrice
            };

            await _productRepository.Save(entity);

            return entity.Id;
        }

        public async Task DeleteProduct(Guid productId)
        {
            await _productRepository.Delete(productId);
        }

        public async Task<ProductOptionDto> GetProductOption(Guid productId, Guid optionId)
        {
            var product = await GetProductFromRepository(productId);
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

        public async Task<IEnumerable<ProductOptionDto>> GetProductOptions(Guid productId)
        {
            var product = await GetProductFromRepository(productId);

            return product.Options.Select(p => new ProductOptionDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                ProductId = productId
            });
        }


        public async Task<Guid> SaveProductOption(ProductOptionDto productOption)
        {
            throw new NotImplementedException();
        }

        public void DeleteProductOption(Guid productId, Guid optionId)
        {
            var product = _productRepository.Get(productId);
            product.RemoveProductOption(optionId);
            _productRepository.Save(product);
        }

        private async Task<Product> GetProductFromRepository(Guid Id)
        {
            var product = await _productRepository.Get(Id);

            if (product == null)
                throw new ProductNotFoundException(Id);

            return product;
        }
    }
}
