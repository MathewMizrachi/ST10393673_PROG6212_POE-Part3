using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        // Register Blob Service Client
        services.AddSingleton(x => {
            string blobConnectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            return new BlobServiceClient(blobConnectionString);
        });

        // Register Queue Service Client
        services.AddSingleton(x => {
            string queueConnectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            return new QueueServiceClient(queueConnectionString);
        });

        // Register Table Service Client
        services.AddSingleton(x => {
            string tableConnectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            return new TableServiceClient(tableConnectionString);
        });
    })
    .Build();

host.Run();
