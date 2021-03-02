using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XeroChallenge.Domain.Entities;

namespace XeroChallenge.Domain.Services
{
    /// <summary>
    /// Represents the interface for the product service
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Get all products by name, this operation uses exact, case-insensitive match, 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<IEnumerable<Product>> GetAllProductsByName(string name);

        /// <summary>
        /// Get the product by product Id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<Product> GetProduct(Guid productId);

        /// <summary>
        /// Delete product using the Id
        /// </summary>
        /// <param name="productIdId"></param>
        /// <returns></returns>
        Task DeleteProduct(Guid productIdId);

        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns></returns>
        Task CreateProduct(Product product);

        /// <summary>
        /// Update existing product
        /// </summary>
        /// <param name="productDto">the product that will be updated</param>
        /// <returns></returns>
        Task UpdateProduct(Product product);

        /// <summary>
        /// Get all the product options that belong to a product
        /// </summary>
        /// <param name="productId">Product Id</param>
        /// <returns></returns>
        Task<IEnumerable<ProductOption>> GetProductOptions(Guid productId);

        /// <summary>
        /// Get a specific product option that belongs to this product
        /// </summary>
        /// <param name="productId">The product Id</param>
        /// <param name="productOptionId">The product option Id</param>
        /// <returns></returns>
        Task<ProductOption> GetProductOption(Guid productId, Guid productOptionId);

        /// <summary>
        /// Create a new product option for this product
        /// </summary>
        /// <param name="productId">The product Id</param>
        /// <param name="productOption">The new product option that will be created</param>
        /// <returns></returns>
        Task CreateProductOption(Guid productId, ProductOption productOption);

        /// <summary>
        /// Update an existing product option for this product
        /// </summary>
        /// <param name="productId">The product Id</param>
        /// <param name="productOption">The product option that will be updated</param>
        /// <returns></returns>
        Task UpdateProductOption(Guid productId, ProductOption productOption);

        /// <summary>
        /// Delete a product option from this product
        /// </summary>
        /// <param name="productId">The product Id</param>
        /// <param name="productOptionId">The product option id for the item that will be deleted</param>
        /// <returns></returns>
        Task DeleteProductOption(Guid productId, Guid productOptionId);
    }
}
