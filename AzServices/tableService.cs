﻿using Azure.Data.Tables;
using PoeSem2.Models;
using System.Threading.Tasks;

namespace CldDev6212.Poe.AzServices
{
    public class tableService
    {
        private readonly TableClient _tableClient;

        public tableService(IConfiguration configuration)
        {
            var connectionString = configuration["AzureStorage:ConnectionString"];
            var serviceClient = new TableServiceClient(connectionString);
            _tableClient = serviceClient.GetTableClient("CustomerProfiles");
            _tableClient.CreateIfNotExists();
        }

        public async Task AddEntityAsync(CustomerProfile profile)
        {
            await _tableClient.AddEntityAsync(profile);
        }

        /*public async Task AddEntity(Product product)
        {
            await _tableClient.AddEntity(product);
        }*/
    }
}