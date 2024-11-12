using Azure.Storage.Files.Shares;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace ST10393673_CLDV6212_POE.Functions
{
    public class FileFunction
    {
        private readonly ShareServiceClient _shareServiceClient;

        public FileFunction(ShareServiceClient shareServiceClient)
        {
            _shareServiceClient = shareServiceClient;
        }

        [Function("UploadToFileShare")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req,
            FunctionContext context)
        {
            var logger = context.GetLogger("UploadToFileShare");

            // Check if the header exists and get the value
            if (!req.Headers.Contains("FileName"))
            {
                logger.LogError("FileName header not found.");
                return req.CreateResponse(System.Net.HttpStatusCode.BadRequest);
            }

            var fileName = req.Headers.GetValues("FileName").FirstOrDefault();

            var shareClient = _shareServiceClient.GetShareClient("myfileshare");
            await shareClient.CreateIfNotExistsAsync();

            var directoryClient = shareClient.GetRootDirectoryClient();
            var fileClient = directoryClient.GetFileClient(fileName);

            try
            {
                // Ensure the request body is not null and contains data
                if (req.Body == null || req.Body.Length == 0)
                {
                    logger.LogError("Request body is empty.");
                    return req.CreateResponse(System.Net.HttpStatusCode.BadRequest);
                }

                await fileClient.CreateAsync(req.Body.Length);
                using (var stream = req.Body)
                {
                    await fileClient.UploadAsync(stream);
                }

                logger.LogInformation($"Uploaded file: {fileName} to Azure File Storage.");
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
