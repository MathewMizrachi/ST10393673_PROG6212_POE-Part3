
using Microsoft.AspNetCore.Mvc;
using ST10393673_CLDV6212_POE.Data;
using ST10393673_CLDV6212_POE.Models;
using System.Threading.Tasks;

namespace ST10393673_CLDV6212_POE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ABCContext _context;

        public HomeController(ABCContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View();

        public IActionResult Products() => View();

        [HttpPost]
        public async Task<IActionResult> UploadProduct(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    ProductId = Guid.NewGuid().ToString(),
                    ProductName = model.ProductName,
                    ProductDescription = model.ProductDescription,
                    ProductPrice = model.ProductPrice,
                    ProductImageUrl = model.ProductImageUrl // Assume the image URL is already set
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return RedirectToAction("Products");
            }

            return View(model);
        }
    }
}





//using Microsoft.AspNetCore.Mvc;
//using ST10393673_CLDV6212_POE.Models;
//using ST10393673_CLDV6212_POE.Services;
//using System.IO;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;

//namespace ST10393673_CLDV6212_POE.Controllers
//{
//    public class HomeController : Controller
//    {
//        private readonly BlobService _blobService;
//        private readonly TableService _tableService;

//        public HomeController(BlobService blobService, TableService tableService)
//        {
//            _blobService = blobService;
//            _tableService = tableService;
//        }

//        public IActionResult Index() => View();

//        public IActionResult Privacy() => View();

//        public IActionResult ContactUs() => View();

//        public IActionResult SpecialOffers() => View();

//        public IActionResult AboutUs() => View();

//        public IActionResult Products() => View();

//        [HttpPost]
//        public async Task<IActionResult> UploadProduct(ProductViewModel model, IFormFile productImage)
//        {
//            if (ModelState.IsValid && productImage != null)
//            {
//                using var stream = productImage.OpenReadStream();
//                var imageUrl = await _blobService.UploadBlobAsync("products-images", productImage.FileName, stream);
//                model.ProductImageUrl = imageUrl;

//                ViewData["Message"] = "Product uploaded successfully!";
//                return RedirectToAction("Products");
//            }

//            ViewData["Message"] = "Please select a valid file to upload.";
//            return View(model);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Register(RegisterViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    await _tableService.AddUserAsync(model);
//                    return RedirectToAction("Index");
//                }
//                catch
//                {
//                    ModelState.AddModelError("", "An error occurred while registering. Please try again.");
//                }
//            }
//            return View(model);
//        }
//    }
//}
