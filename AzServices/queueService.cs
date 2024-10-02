using Azure.Storage.Queues;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CldDev6212.Poe.AzServices
{
    public class queueService
    {
        private readonly QueueServiceClient _queueServiceClient;
       


        public queueService(IConfiguration configuration)
        {
            
            _queueServiceClient = new QueueServiceClient(configuration["AzureStorage:ConnectionString"]);
        }
        /*public async Task AddMessageToQueue(string queueName, string message)
        {
            var queueClient = new QueueClient(_connectionString, queueName);
            await queueClient.CreateIfNotExistsAsync();
            await queueClient.SendMessageAsync(message);
        }
        */
       
          public async Task SendMessageAsync(string queueName, string message)
        {
            var queueClient = _queueServiceClient.GetQueueClient(queueName);
            await queueClient.CreateIfNotExistsAsync();
            await queueClient.SendMessageAsync(message);
        } 
    }
}
