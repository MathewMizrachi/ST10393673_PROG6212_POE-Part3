using System.IO;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Azure.Data.Tables;
using ST10393673_CLDV6212_POE.Models;
using System.Threading.Tasks;

namespace ST10393673_CLDV6212_POE_Functions.Functions
{
    public class StoreToTableFunction
    {
        private readonly TableServiceClient _tableServiceClient;

        // Constructor to inject TableServiceClient
        public StoreToTableFunction(TableServiceClient tableServiceClient)
        {
            _tableServiceClient = tableServiceClient;
        }

        [Function("StoreToTableFunction")]
        public async Task Run(
            [BlobTrigger("products/{name}")] Stream myBlob,
            string name,
            FunctionContext context)
        {
            var logger = context.GetLogger("StoreToTableFunction");
            logger.LogInformation($"Blob trigger for table storage. Blob Name: {name}");

            // Access the table using the injected TableServiceClient
            TableClient customerTable = _tableServiceClient.GetTableClient("Customers");

            // Read from the blob (you may implement logic to deserialize blob content)
            // For this example, we are creating a dummy customer profile
            var customer = new CustomerProfile
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "1234567890"
            };

            // Insert customer into the Azure Table
            await customerTable.AddEntityAsync(customer);
        }
    }
}
