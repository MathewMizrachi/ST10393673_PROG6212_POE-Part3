
using Microsoft.AspNetCore.Mvc;
using ST10393673_CLDV6212_POE.Data;  // Include DbContext
using Microsoft.EntityFrameworkCore;  // For ToListAsync extension
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;  // For Path
using ST10393673_CLDV6212_POE.Models;

namespace ST10393673_CLDV6212_POE.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ABCContext _context;  // DbContext to interact with the database

        public OrdersController(ABCContext context)
        {
            _context = context;
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
                // Process the order and save it to the database
                var order = new Order
                {
                    OrderId = orderId,
                    Status = "Processing",  // Set the status
                    DateProcessed = DateTime.UtcNow  // Set the processed date
                };

                _context.Orders.Add(order);  // Add order to database
                await _context.SaveChangesAsync();  // Save changes to the database

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
                // Assuming the file is uploaded to a storage service, or you could store file paths in the database
                var filePath = await SaveFileAsync(file);  // Implement file storage logic here
                ViewData["Message"] = "Contract uploaded successfully!";
            }
            catch
            {
                ViewData["Message"] = "An error occurred while uploading the contract.";
            }

            return View();
        }

        private async Task<string> SaveFileAsync(IFormFile file)
        {
            // Simulate saving the file and returning the path
            var filePath = "/uploads/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
            // You can save the file to your storage service here (e.g., local file system, Azure Blob, etc.)
            return filePath;
        }
    }
}







//using Microsoft.AspNetCore.Mvc;
//using ST10393673_CLDV6212_POE.Services;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using System;

//namespace ST10393673_CLDV6212_POE.Controllers
//{
//    public class OrdersController : Controller
//    {
//        private readonly QueueService _queueService;
//        private readonly FileService _fileService;

//        public OrdersController(QueueService queueService, FileService fileService)
//        {
//            _queueService = queueService;
//            _fileService = fileService;
//        }

//        public IActionResult ProcessOrder() => View();

//        [HttpPost]
//        public async Task<IActionResult> ProcessOrder(string orderId)
//        {
//            if (string.IsNullOrEmpty(orderId))
//            {
//                ViewData["Message"] = "Order ID cannot be empty.";
//                return View();
//            }

//            try
//            {
//                await _queueService.SendMessageAsync("order-processing", $"Processing order {orderId}");
//                ViewData["Message"] = $"Order {orderId} is being processed.";
//            }
//            catch
//            {
//                ViewData["Message"] = "An error occurred while processing the order.";
//            }

//            return View();
//        }

//        public IActionResult UploadContract() => View();

//        [HttpPost]
//        public async Task<IActionResult> UploadContract(IFormFile file)
//        {
//            if (file == null || file.Length == 0)
//            {
//                ViewData["Message"] = "Please select a file to upload.";
//                return View();
//            }

//            try
//            {
//                using var stream = file.OpenReadStream();
//                await _fileService.UploadFileAsync("contracts-logs", file.FileName, stream);
//                ViewData["Message"] = "Contract uploaded successfully!";
//            }
//            catch
//            {
//                ViewData["Message"] = "An error occurred while uploading the contract.";
//            }

//            return View();
//        }
//    }
//}
