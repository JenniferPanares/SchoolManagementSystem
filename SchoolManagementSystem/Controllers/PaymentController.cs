using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;
using System.Collections.Generic;
using SchoolManagementSystem.ViewModels;

namespace SchoolManagementSystem.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IConfiguration _configuration;

        // Constructor that injects IConfiguration to get access to settings
        public PaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Payment view to initiate the payment
        public IActionResult Pay()
        {
            ViewBag.StripePublishableKey = _configuration["APIKeys:StripePublishableKey"];
            return View(new PaymentViewModel()); 
        }

        // Create Stripe session for the payment
        [HttpPost]
        public IActionResult CreateSession(PaymentViewModel paymentViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.StripePublishableKey = _configuration["APIKeys:StripePublishableKey"];
                return View("Pay", paymentViewModel);
            }

            var domain = $"{this.Request.Scheme}://{this.Request.Host}";
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = paymentViewModel.Amount * 100, // Stripe expects amounts in cents
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "School Fee Payment"
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",
                SuccessUrl = $"{domain}/Payment/Success",
                CancelUrl = $"{domain}/Payment/Cancel"
            };
            var service = new SessionService();
            Session session = service.Create(options);

            return Json(new { id = session.Id });
        }

        // Success action for successful payments
        public IActionResult Success()
        {
            return View();
        }

        // Cancel action for canceled payments
        public IActionResult Cancel()
        {
            return View();
        }
    }
}
