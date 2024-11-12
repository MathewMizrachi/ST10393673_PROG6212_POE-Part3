using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.IO;
using System;
using Microsoft.EntityFrameworkCore;
using ST10393673_CLDV6212_POE.Models;
using ST10393673_CLDV6212_POE.Data;  // DbContext for database interactions

namespace ST10393673_CLDV6212_POE.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ABCContext _context;  // DbContext to interact with the database

        public ProductsController(ABCContext context)
        {
            _context = context;
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View(new ProductViewModel());
        }

        // POST: Products/Create
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Handle image upload and set ProductImageUrl if an image is uploaded
                if (model.ProductImage != null)
                {
                    // Generate a unique file name using GUID
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ProductImage.FileName);
                    // Save image to storage and get the URL
                    var imageUrl = await SaveImageToStorageAsync(model.ProductImage, fileName);

                    // Set the ProductImageUrl in the view model
                    model.ProductImageUrl = imageUrl;
                }

                // Create a new product object to store in the database
                var product = new Product
                {
                    ProductId = Guid.NewGuid().ToString(),
                    ProductName = model.ProductName,
                    ProductDescription = model.ProductDescription,
                    ProductPrice = model.ProductPrice,
                    ProductImageUrl = model.ProductImageUrl
                };

                // Add the new product to the database
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));  // Redirect to the Index view
            }

            return View(model); // Return the model to the view if the model is not valid
        }

        private async Task<string> SaveImageToStorageAsync(IFormFile productImage, string fileName)
        {
            // Simulate saving the image and returning the URL (could be saved to a local directory, cloud storage, etc.)
            var imageUrl = "/images/" + fileName;
            // In practice, save the image to your storage system here (e.g., Azure Blob Storage, local server, etc.)
            return imageUrl;
        }

        // GET: Products/Index (Listing all products)
        public async Task<IActionResult> Index()
        {
            // Fetch products from the database
            var products = await _context.Products.ToListAsync();

            return View(products);  // Pass the product list to the view
        }
    }
}








//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Http;
//using System.Threading.Tasks;
//using System.IO;
//using System;
//using ST10393673_CLDV6212_POE.Models;
//using ST10393673_CLDV6212_POE.Services;

//namespace ST10393673_CLDV6212_POE.Controllers
//{
//    public class ProductsController : Controller
//    {
//        private readonly BlobService _blobService;  // Injecting BlobService to handle image uploads

//        public ProductsController(BlobService blobService)
//        {
//            _blobService = blobService;
//        }

//        // GET: Products/Create
//        public IActionResult Create()
//        {
//            return View(new ProductViewModel());
//        }

//        // POST: Products/Create
//        [HttpPost]
//        public async Task<IActionResult> Create(ProductViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                // Handle image upload and set ProductImageUrl if an image is uploaded
//                if (model.ProductImage != null)
//                {
//                    // Generate a unique blob name using GUID and the file extension
//                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ProductImage.FileName);
//                    // Define container name for blobs
//                    var containerName = "product-images";  // You can change this as needed
//                    var imageUrl = await SaveImageToBlobStorage(model.ProductImage, containerName, fileName);

//                    // Set the ProductImageUrl in the view model
//                    model.ProductImageUrl = imageUrl;
//                }

//                // Create a new product object using the ProductViewModel
//                var product = new Product
//                {
//                    ProductId = Guid.NewGuid().ToString(),  // Ensure the product has a unique ID
//                    ProductName = model.ProductName,
//                    ProductDescription = model.ProductDescription,
//                    ProductPrice = model.ProductPrice,
//                    ProductImageUrl = model.ProductImageUrl  // Store the image URL from Blob Storage
//                };

//                // You can save the product to your database or storage here, e.g., Azure Table Storage, SQL database, etc.
//                // Example: await _productService.AddProductAsync(product);

//                return RedirectToAction(nameof(Index)); // Redirect to the Index action after successful creation
//            }

//            return View(model); // Return the model to the view if the model is not valid
//        }

//        private async Task<string> SaveImageToBlobStorage(IFormFile productImage, string containerName, string fileName)
//        {
//            if (productImage != null && productImage.Length > 0)
//            {
//                // Use the BlobService to upload the image to Blob Storage
//                using (var stream = productImage.OpenReadStream())
//                {
//                    var imageUrl = await _blobService.UploadBlobAsync(containerName, fileName, stream);
//                    return imageUrl;  // Return the URL of the uploaded file
//                }
//            }

//            return null; // Return null if no image is uploaded
//        }

//        // GET: Products/Index (Listing all products)
//        public IActionResult Index()
//        {
//            // Example: Fetch the products from your data store
//            // var products = await _productService.GetProductsAsync();

//            return View();  // Pass the product list to the view
//        }
//    }
//}
