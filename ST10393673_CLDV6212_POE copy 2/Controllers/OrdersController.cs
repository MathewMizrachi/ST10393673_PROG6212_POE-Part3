using Microsoft.AspNetCore.Mvc;
using ST10393673_CLDV6212_POE.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;

namespace ST10393673_CLDV6212_POE.Controllers
{
    public class OrdersController : Controller
    {
        private readonly QueueService _queueService;
        private readonly FileService _fileService;

        public OrdersController(QueueService queueService, FileService fileService)
        {
            _queueService = queueService;
            _fileService = fileService;
        }

        public IActionResult ProcessOrder() => View();

        [HttpPost]
        public async Task<IActionResult> ProcessOrder(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                ViewData["Message"] = "Order ID cannot be empty.";
                return View();
            }

            try
            {
                await _queueService.SendMessageAsync("order-processing", $"Processing order {orderId}");
                ViewData["Message"] = $"Order {orderId} is being processed.";
            }
            catch
            {
                ViewData["Message"] = "An error occurred while processing the order.";
            }

            return View();
        }

        public IActionResult UploadContract() => View();

        [HttpPost]
        public async Task<IActionResult> UploadContract(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ViewData["Message"] = "Please select a file to upload.";
                return View();
            }

            try
            {
                using var stream = file.OpenReadStream();
                await _fileService.UploadFileAsync("contracts-logs", file.FileName, stream);
                ViewData["Message"] = "Contract uploaded successfully!";
            }
            catch
            {
                ViewData["Message"] = "An error occurred while uploading the contract.";
            }

            return View();
        }
    }
}
