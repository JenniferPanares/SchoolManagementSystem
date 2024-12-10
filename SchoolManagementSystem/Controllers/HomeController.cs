using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;

        // Constructor
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TuitionPayment()
        {
            return View();
        }

        public IActionResult TuitionBilling(string studentId, string lastName)
        {
            if (string.IsNullOrEmpty(studentId) || string.IsNullOrEmpty(lastName))
            {
                return RedirectToAction("TuitionPayment");
            }

            return View();
        }


        public IActionResult About()
        {
            return View();
        }

        public IActionResult AerialView()
        {
            var apiKey = _configuration["APIKeys:GoogleMaps"]; // Get the API key from appsettings.json
            ViewData["GoogleMapsApiKey"] = apiKey; // Pass it to the view
            return View("~/Views/Map/AerialView.cshtml"); // Explicitly specify the view path
        }


        public IActionResult Programs()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
