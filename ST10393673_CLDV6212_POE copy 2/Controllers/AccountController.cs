using Microsoft.AspNetCore.Mvc;
using ST10393673_CLDV6212_POE.Models;
using ST10393673_CLDV6212_POE.Services;

namespace ST10393673_CLDV6212_POE.Controllers
{
    public class AccountController : Controller
    {
        private readonly TableService _tableService;

        public AccountController(TableService tableService)
        {
            _tableService = tableService;
        }

        // GET: Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _tableService.AddUserAsync(model); // Assuming AddUserAsync adds the user to your table
                    return RedirectToAction("Index", "Home"); // Redirect to Home after successful registration
                }
                catch
                {
                    ModelState.AddModelError("", "An error occurred while registering. Please try again.");
                }
            }
            return View(model);
        }
    }
}
