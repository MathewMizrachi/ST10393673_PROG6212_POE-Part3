using Azure.Storage.Blobs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace ST10393673_CLDV6212_POE.Functions
{
    public class BlobFunction
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobFunction(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        [Function("UploadToBlob")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req,
            FunctionContext context)
        {
            var logger = context.GetLogger("UploadToBlob");
            string fileName = null;

            // Check if the header exists and get the value
            if (req.Headers.Contains("FileName"))
            {
                fileName = req.Headers.GetValues("FileName").FirstOrDefault(); // Get the first value
            }

            if (string.IsNullOrEmpty(fileName))
            {
                logger.LogError("FileName header not found or is empty.");
                return req.CreateResponse(System.Net.HttpStatusCode.BadRequest);
            }

            var blobContainerClient = _blobServiceClient.GetBlobContainerClient("mycontainer");
            await blobContainerClient.CreateIfNotExistsAsync();

            var blobClient = blobContainerClient.GetBlobClient(fileName);

            try
            {
                if (req.Body == null || req.Body.Length == 0)
                {
                    logger.LogError("Request body is empty.");
                    return req.CreateResponse(System.Net.HttpStatusCode.BadRequest);
                }

                using (var stream = req.Body)
                {
                    await blobClient.UploadAsync(stream, new Azure.Storage.Blobs.Models.BlobHttpHeaders { ContentType = "application/octet-stream" });
                }

                logger.LogInformation($"Uploaded file: {fileName} to Blob Storage.");
                var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
                response.WriteString($"File '{fileName}' uploaded successfully.");
                return response;
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occurred while uploading the file: {ex.Message}");
                return req.CreateResponse(System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
