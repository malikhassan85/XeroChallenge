using System;
using System.Collections.Generic;
using System.Text;
using XeroChallenge.Application.DTOs;

namespace XeroChallenge.Application.Services
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetAllProducts();

        ProductDto GetProduct(Guid productId);

        void DeleteProduct(Guid productIdId);

        Guid SaveProduct(ProductDto product);

        IEnumerable<ProductOptionDto> GetProductOptions(Guid productId);

        ProductOptionDto GetProductOption(Guid productId, Guid optionId);

        Guid SaveProductOption(ProductOptionDto productOption);

        void DeleteProductOption(Guid productId, Guid optionId);
    }
}
