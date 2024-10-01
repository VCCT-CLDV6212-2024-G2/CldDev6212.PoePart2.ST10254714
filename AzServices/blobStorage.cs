using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using PoeSem2.Models;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace CldDev6212.Poe.AzServices
{
    public class blobStorage
    {
        private readonly BlobServiceClient _blobServiceClient;

        public blobStorage(IConfiguration configuration)
        {
            _blobServiceClient = new BlobServiceClient(configuration["AzureStorage:ConnectionString"]);
        }


        public async Task UploadBlobAsync(string containerName, string blobName, Stream content)
        {
            try
            {
                var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
                await containerClient.CreateIfNotExistsAsync();
                var blobClient = containerClient.GetBlobClient(blobName);
                await blobClient.UploadAsync(content, true);


            }
            catch (Exception ex)
            {
                Console.WriteLine($"blob has failed to upload: {ex.Message}");
                throw;
            }
        }
    }

    public class imageService
    {
        private readonly blobStorage _blobStorage;

        public imageService(blobStorage blobStorage)
        {
            _blobStorage = blobStorage;
        }

        public async Task uploadImageAsync(string containerName, string imageName, Stream imageStream)
        {
            await _blobStorage.UploadBlobAsync(containerName, imageName, imageStream);
        }

    }


}




