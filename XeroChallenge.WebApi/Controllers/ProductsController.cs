using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XeroChallenge.Domain.Exceptions;
using XeroChallenge.Domain.Services;
using XeroChallenge.WebApi.Extensions;
using XeroChallenge.WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XeroChallenge.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string name)
        {
            var productsList = await _productService.GetAllProductsByName(name);
            return Ok(productsList.ToList().ToDto());
        }

        // GET api/<ProductsController>/DF2E9176-35EE-4F0A-AE55-83023D2DB1A3
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var product = await _productService.GetProduct(id);
                return Ok(product.ToDto());
            }
            catch (ProductNotFoundException)
            {
                return NotFound();
            }
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductDto productDto)
        {
             await _productService.CreateProduct(productDto.ToDomainEntity());
            return Ok();
        }

        // PUT api/<ProductsController>/DF2E9176-35EE-4F0A-AE55-83023D2DB1A3
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, ProductDto productDto)
        {
            productDto.Id = id;
            await _productService.UpdateProduct(productDto.ToDomainEntity());
            return Ok();
        }

        // DELETE api/<ProductsController>/DF2E9176-35EE-4F0A-AE55-83023D2DB1A3
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _productService.DeleteProduct(id);
            return Ok();
        }

        [HttpGet("{id}/options")]
        public async Task<IActionResult> GetProductOptions(Guid id)
        {
            var productOptions = await _productService.GetProductOptions(id);
            return Ok(productOptions.ToList().ToDto());
        }

        [HttpGet("{id}/options/{optionId}")]
        public async Task<IActionResult> GetProductOption(Guid id, Guid optionId)
        {
            try
            {
                var productOption = await _productService.GetProductOption(id, optionId);
                return Ok(productOption.ToDto());
            }
            catch(ProductOptionNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost("{id}/options/")]
        public async Task<IActionResult> CreateProductOption(Guid id, ProductOptionDto productOption)
        {
            await _productService.CreateProductOption(id, productOption.ToDomainEntity());
            return Ok();
        }

        [HttpPut("{id}/options/{optionId}")]
        public async Task<IActionResult> UpdateProductOption(Guid id, Guid optionId, ProductOptionDto productOption)
        {
            productOption.Id = optionId;
            await _productService.UpdateProductOption(id, productOption.ToDomainEntity());
            return Ok();
        }

        [HttpDelete("{id}/options/{optionId}")]
        public async Task<IActionResult> DeleteProductOption(Guid id, Guid optionId)
        {
             await _productService.DeleteProductOption(id, optionId);
            return Ok();
        }
    }
}
