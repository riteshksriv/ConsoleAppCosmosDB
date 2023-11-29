using System.Diagnostics;
using Microsoft.Azure.Cosmos;

namespace ConsoleAppCosmosDB
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Debug.WriteLine("Hello, World!");
            await Task.FromResult("Hello World!");
        }

        public static async Task Run(string connectionString)
        {
            var clientOptions = new CosmosClientOptions
            {
                ConnectionMode = ConnectionMode.Gateway,
                HttpClientFactory = () =>
                {
                    HttpMessageHandler httpMessageHandler = new HttpClientHandler()
                    {
                        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                    };
                    return new HttpClient(httpMessageHandler);
                }
            };
            var cosmosClient = new CosmosClient(connectionString, clientOptions);
            var dbResponse = await cosmosClient.CreateDatabaseIfNotExistsAsync("MyDatabase");
            await dbResponse.Database.CreateContainerIfNotExistsAsync("MyTable", "/id");
        }
    }
}