using Microsoft.AspNetCore.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class AccountSummaryController : Controller
    {
        [HttpGet]
        public IActionResult TuitionBilling(string studentId, string lastName)
        {
            // Optional: Pass data to the view (for Option 1)
            ViewData["StudentId"] = studentId;
            ViewData["LastName"] = lastName;

            return View(); // This assumes a corresponding view exists
        }
    }
}
