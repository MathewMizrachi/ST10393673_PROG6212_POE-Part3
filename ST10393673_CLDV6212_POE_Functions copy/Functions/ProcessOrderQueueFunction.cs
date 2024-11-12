using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ST10393673_CLDV6212_POE_Functions.Functions
{
    public static class ProcessOrderQueueFunction
    {
        [Function("ProcessOrderQueueFunction")]
        public static async Task Run(
            [QueueTrigger("order-queue")] string orderMessage,  // Use QueueTrigger attribute from Azure Functions Worker
            FunctionContext context)
        {
            var logger = context.GetLogger("ProcessOrderQueueFunction");
            logger.LogInformation($"Order message received: {orderMessage}");

            // Simulate order processing logic
            await Task.Delay(2000);  // Simulating a delay for order processing

            logger.LogInformation($"Order processing completed for: {orderMessage}");
        }
    }
}
