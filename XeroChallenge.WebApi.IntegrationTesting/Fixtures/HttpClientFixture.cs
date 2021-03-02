using System;
using System.Net.Http;

namespace XeroChallenge.WebApi.IntegrationTesting.Fixtures
{
    public class HttpClientFixture : IDisposable
    {
        public HttpClientFixture()
        {
            // Determine if specific configuration has been provided
            string endpoint = Environment.GetEnvironmentVariable("WebApi_ENDPOINT") ?? "http://localhost:5000";

            var clientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
                {
                    return true;
                }
            };

            // Initialise a HTTP client
            this.HttpClient = new HttpClient(clientHandler);
            this.HttpClient.BaseAddress = new Uri(endpoint);
        }

        public HttpClient HttpClient { get; }

        public void Dispose()
        {
            this.HttpClient?.Dispose();
        }
    }
}
