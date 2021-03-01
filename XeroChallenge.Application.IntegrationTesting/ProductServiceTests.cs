using Microsoft.Extensions.Configuration;
using System;
using XeroChallenge.Application.Services;
using XeroChallenge.Domain.Repositories;
using XeroChallenge.Infrastructure.DataAccess;
using Xunit;

namespace XeroChallenge.Application.IntegrationTesting
{
    public class ProductServiceTests
    {
        [Fact]
        public void GetProduct_ReturnExistingProduct()
        {
            var configuration = GetConfigurationProvider();
            var productRepository = new ProductRepository(configuration);
            var productService = new ProductService(productRepository);

            var item = productService.GetProduct(Guid.Parse("8F2E9176-35EE-4F0A-AE55-83023D2DB1A3"));
        }

        private IConfiguration GetConfigurationProvider()
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json", false);

            return configurationBuilder.Build();
        }
    }
}
