using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XeroChallenge.Application.DTOs;

namespace XeroChallenge.Application.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsByName(string name);

        Task<ProductDto> GetProduct(Guid productId);

        Task DeleteProduct(Guid productIdId);

        Task<Guid> CreateProduct(ProductDto productDto);

        Task<Guid> UpdateProduct(ProductDto productDto);

        Task<IEnumerable<ProductOptionDto>> GetProductOptions(Guid productId);

        Task<ProductOptionDto> GetProductOption(Guid productId, Guid optionId);

        Task<Guid> SaveProductOption(ProductOptionDto productOption);

        Task DeleteProductOption(Guid productId, Guid optionId);
    }
}
