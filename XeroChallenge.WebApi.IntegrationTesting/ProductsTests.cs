using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XeroChallenge.WebApi.IntegrationTesting.Fixtures;
using XeroChallenge.WebApi.Models;
using Xunit;

namespace XeroChallenge.WebApi.IntegrationTesting
{
    [Trait("Type", "Integration")]
    public class ProductsTests : IClassFixture<HttpClientFixture>
    {
        public ProductsTests(HttpClientFixture fixture)
        {
            this.Fixture = fixture;
        }
  
        protected HttpClientFixture Fixture { get; }

        [Fact]
        public async Task GetListOfProducts_ReturnOkStatusCode()
        {
            // Prepare
            HttpClient client = this.Fixture.HttpClient;

            //Act
            var endpoint = string.Format("api/products");
            HttpResponseMessage queryProductsHttpResponse = await client.GetAsync(endpoint).ConfigureAwait(false);

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, queryProductsHttpResponse.StatusCode);

            var productsListString = await queryProductsHttpResponse.Content.ReadAsStringAsync();
            var listOfProducts = JsonConvert.DeserializeObject<List<ProductDto>>(productsListString);

            Assert.NotNull(listOfProducts);
            Assert.All(listOfProducts, p =>
            {
                Assert.NotEqual(Guid.Empty, p.Id);
            });
        }

        [Fact]
        public async Task GetUnAvailableProduct_ReturnNotFoundStatusCode()
        {
            // Prepare
            HttpClient client = this.Fixture.HttpClient;

            //Act
            var endpoint = string.Format("api/products/{0}", Guid.NewGuid());
            HttpResponseMessage queryProductsHttpResponse = await client.GetAsync(endpoint).ConfigureAwait(false);

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.NotFound, queryProductsHttpResponse.StatusCode);
        }

        [Fact]
        public async Task CreateNewProduct_ReturnOkStatusCode()
        {
            // Prepare
            HttpClient client = this.Fixture.HttpClient;
            var productDto = new ProductDto
            {
                Name = "New Mobile IPhone 12",
                Description = "A mobile that fits your needs all the day",
                Price = 2000.50m,
                DeliveryPrice = 100
            };
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(productDto));
            var httpContent= new StringContent(stringPayload, Encoding.UTF8, "application/json");

            //Act
            var endpoint = string.Format("api/products/");
            HttpResponseMessage queryProductsHttpResponse = await client.PostAsync(endpoint, httpContent).ConfigureAwait(false);

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, queryProductsHttpResponse.StatusCode);
        }
    }
}
