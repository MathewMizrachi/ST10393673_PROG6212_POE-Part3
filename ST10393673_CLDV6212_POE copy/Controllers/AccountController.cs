// EF Core Version - Using a Relational Database (e.g., SQL Server)
// This version interacts with a relational database through Entity Framework Core (EF Core).
// It performs user registration by saving the user's data to a database using the EF Core context (ABCContext).

using Microsoft.AspNetCore.Mvc;
using ST10393673_CLDV6212_POE.Data;
using ST10393673_CLDV6212_POE.Models;
using System.Threading.Tasks;

namespace ST10393673_CLDV6212_POE.Controllers
{
    public class AccountController : Controller
    {
        private readonly ABCContext _context;

        public AccountController(ABCContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customerProfile = new CustomerProfile
                {
                    CustomerId = Guid.NewGuid().ToString(),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };

                _context.CustomerProfiles.Add(customerProfile);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}


// Azure Table Storage Version - Using Azure Table Storage (NoSQL)
// This version stores user registration data in Azure Table Storage, a NoSQL cloud storage service.




//using Microsoft.AspNetCore.Mvc;
//using ST10393673_CLDV6212_POE.Models;
//using ST10393673_CLDV6212_POE.Services;

//namespace ST10393673_CLDV6212_POE.Controllers
//{
//    public class AccountController : Controller
//    {
//        private readonly TableService _tableService;

//        public AccountController(TableService tableService)
//        {
//            _tableService = tableService;
//        }

//        // GET: Register
//        public IActionResult Register()
//        {
//            return View();
//        }

//        // POST: Register
//        [HttpPost]
//        public async Task<IActionResult> Register(RegisterViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    await _tableService.AddUserAsync(model); // Assuming AddUserAsync adds the user to your table
//                    return RedirectToAction("Index", "Home"); // Redirect to Home after successful registration
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
