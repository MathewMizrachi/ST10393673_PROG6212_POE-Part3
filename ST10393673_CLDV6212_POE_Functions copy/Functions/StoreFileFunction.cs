using System.IO;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Azure.Storage.Files.Shares;
using System.Threading.Tasks;
using System.Net;

namespace ST10393673_CLDV6212_POE_Functions.Functions
{
    public static class StoreFileFunction
    {
        [Function("StoreFileFunction")]
        public static async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("StoreFileFunction");
            logger.LogInformation("C# HTTP trigger function processed a request for file upload.");

            // Get the file content from the HTTP request
            string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            ShareServiceClient fileShareClient = new ShareServiceClient(connectionString);
            ShareClient shareClient = fileShareClient.GetShareClient("contracts-logs");
            ShareDirectoryClient directoryClient = shareClient.GetRootDirectoryClient();

            // Use a memory stream to handle the file upload
            using (var memoryStream = new MemoryStream())
            {
                await req.Body.CopyToAsync(memoryStream);
                var fileName = "uploadedfile.txt";  // Replace with actual logic to get the file name

                ShareFileClient fileClient = directoryClient.GetFileClient(fileName);
                memoryStream.Position = 0; // Reset stream position
                await fileClient.CreateAsync(memoryStream.Length);
                await fileClient.UploadAsync(memoryStream);
            }

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteString($"File uploaded successfully.");
            return response;
        }
    }
}
