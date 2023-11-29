using System.Diagnostics;
using ConsoleAppCosmosDB;
using DotNet.Testcontainers.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Testcontainers.CosmosDb;

namespace IntegrationTests
{
    [TestClass]
    public class UnitTest1
    {
        const string COSMOSDB_IMAGE = "mcr.microsoft.com/cosmosdb/linux/azure-cosmos-emulator:latest";
        static string connString;
        static CosmosDbContainer cosmosDb;

        [ClassInitialize]
        public static async Task ClassInitialize(TestContext context)
        {
            var cosmosDb = new CosmosDbBuilder()
                .WithImage(COSMOSDB_IMAGE)
                .Build();
            await cosmosDb.StartAsync();
            connString = cosmosDb.GetConnectionString();
        }

        [ClassCleanup]
        public static async Task ClassCleanup()
        {
            await cosmosDb.StopAsync();
        }

        [TestMethod]
        public async Task TestMethod1()
        {
            await Program.Run(connString);
            Assert.IsTrue(true);
        }
    }
}