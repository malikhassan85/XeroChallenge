using System;
using System.Collections.Generic;
using XeroChallenge.Domain.Repositories;
using System.Linq;
using XeroChallenge.Domain.Entities;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using XeroChallenge.Domain.Exceptions;

namespace XeroChallenge.Domain.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private ILogger<ProductService> _Logger;
        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _Logger = logger;
        }

        public async Task<IEnumerable<Product>> GetAllProductsByName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                _Logger.LogInformation("Call repository to get product/s based on name value: {name}", name);
                return await _productRepository.GetAllByName(name);
            }
            else
            {
                _Logger.LogInformation("Call repository to get all products");
                return await _productRepository.GetAll();
            }
        }

        public async Task<Product> GetProduct(Guid productId)
        {
            return await GetProductFromRepository(productId);
        }

        public async Task CreateProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            if (!product.IsNew)
                throw new ArgumentException(nameof(product.Id), "Id should be empty when creating a new product");

            _Logger.LogInformation("Call repository to create a product");
            await _productRepository.Save(product);
        }

        public async Task UpdateProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            if (product.IsNew)
                throw new ArgumentException(nameof(product.Id), "Id can't be empty when updating an existing product");

            _Logger.LogInformation("Call repository to update product: {Id}", product.Id);
            await _productRepository.Save(product);
        }

        public async Task DeleteProduct(Guid productId)
        {
            _Logger.LogInformation("Call repository to delete product: {productId}", productId);
            await _productRepository.Delete(productId);
        }

        public async Task<ProductOption> GetProductOption(Guid productId, Guid optionId)
        {
            var product = await GetProductFromRepository(productId);
            var productOption = product.Options.SingleOrDefault(p => p.Id == optionId);

            if (productOption == null)
                throw new ProductOptionNotFoundException(optionId);

            _Logger.LogInformation("Return productOption: {Id} for product: {productId}", optionId, productId);
            return productOption;
        }

        public async Task<IEnumerable<ProductOption>> GetProductOptions(Guid productId)
        {
            var product = await GetProductFromRepository(productId);

            _Logger.LogInformation("Return product options for product: {Id}", productId);
            return product.Options;
        }

        public async Task CreateProductOption(Guid productId, ProductOption productOption)
        {
            var product = await GetProductFromRepository(productId);

            _Logger.LogInformation("Add product option for product: {Id}", productId);
            product.AddProductOption(productOption);
            await _productRepository.Save(product);
        }

        public async Task UpdateProductOption(Guid productId, ProductOption productOption)
        {
            var product = await GetProductFromRepository(productId);
            var originalOption = product.Options.SingleOrDefault(p => p.Id == productOption.Id);

            if (originalOption == null)
                throw new ProductOptionNotFoundException(productOption.Id);

            _Logger.LogInformation("Update product option for product: {Id} and productOption:{productOption}", productId, productOption.Id);
            originalOption.UpdateValues(productOption);
            await _productRepository.Save(product);
        }

        public async Task DeleteProductOption(Guid productId, Guid optionId)
        {
            var product = await GetProductFromRepository(productId);

            _Logger.LogInformation("Remove productOption: {optionId} from product: {Id}", optionId, productId);
            product.RemoveProductOption(optionId);
            await _productRepository.Save(product);
        }

        private async Task<Product> GetProductFromRepository(Guid Id)
        {
            _Logger.LogInformation("Call repository to get product: {Id}", Id);
            var product = await _productRepository.Get(Id);

            if (product == null)
                throw new ProductNotFoundException(Id);

            return product;
        }

    }
}
