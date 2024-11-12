using System.IO;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Azure.Storage.Blobs;
using System.Threading.Tasks;
using System.Net;

namespace ST10393673_CLDV6212_POE_Functions.Functions
{
    public static class StoreImageFunction
    {
        [Function("StoreImageFunction")]
        public static async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("StoreImageFunction");
            logger.LogInformation("C# HTTP trigger function processed a request for image upload.");

            string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("product-images");

            // Use a memory stream to handle the file upload
            using (var memoryStream = new MemoryStream())
            {
                await req.Body.CopyToAsync(memoryStream);
                var fileName = "uploadedimage.jpg";  // Replace with actual logic to get the file name

                BlobClient blobClient = containerClient.GetBlobClient(fileName);
                memoryStream.Position = 0; // Reset stream position
                await blobClient.UploadAsync(memoryStream, overwrite: true);
            }

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteString($"Image uploaded successfully.");
            return response;
        }
    }
}
