using Azure;
using Azure.Data.Tables;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;


namespace ST10393673_CLDV6212_POE.Functions
{

    public class TableFunction
    {
        private readonly TableServiceClient _tableServiceClient;

        public TableFunction(TableServiceClient tableServiceClient)
        {
            _tableServiceClient = tableServiceClient;
        }

        [Function("InsertToTable")]
        public async Task Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequestData req,
            FunctionContext context)
        {
            var logger = context.GetLogger("InsertToTable");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonSerializer.Deserialize<MyEntity>(requestBody);

            var tableClient = _tableServiceClient.GetTableClient("MyTable");

            await tableClient.CreateIfNotExistsAsync();

            await tableClient.AddEntityAsync(data);

            logger.LogInformation($"Inserted entity with PartitionKey: {data.PartitionKey}, RowKey: {data.RowKey}");
        }

        public class MyEntity : ITableEntity
        {
            public string PartitionKey { get; set; }
            public string RowKey { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public DateTimeOffset? Timestamp { get; set; }
            public ETag ETag { get; set; }
        }
    }
}