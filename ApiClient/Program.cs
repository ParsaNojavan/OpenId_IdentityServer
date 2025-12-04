using Duende.IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HttpClient httpClient = new HttpClient();
            var discoveryDocument = httpClient
                .GetDiscoveryDocumentAsync("https://localhost:7030").Result;
            if (discoveryDocument.IsError)
            {
                Console.WriteLine(discoveryDocument.Error);
                return;
            }

            var accessToken = httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = "HavaApi",
                ClientSecret = "123456",
                Scope = "HavaShenasiApi"
            }).Result;

            if (accessToken.IsError)
            {
                Console.WriteLine(accessToken.Error);
                return;
            }

            var apiClient = new HttpClient();

            apiClient.SetBearerToken(accessToken.AccessToken);
            var data = apiClient.GetAsync("https://localhost:7217/WeatherForecast").Result;

            Console.WriteLine(data.Content.ReadAsStringAsync().Result);

            Console.ReadKey();

        }
    }
}
