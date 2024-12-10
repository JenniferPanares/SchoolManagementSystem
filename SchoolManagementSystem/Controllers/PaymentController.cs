using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

[ApiController]
[Route("api/payment")]
public class PaymentController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public PaymentController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("create-session")]
    public IActionResult CreateSession([FromBody] PaymentRequest request)
    {
        if (request.Amount <= 0)
        {
            return BadRequest("Invalid payment amount.");
        }

        StripeConfiguration.ApiKey = _configuration["StripeSecretKey"];

        var domain = $"{Request.Scheme}://{Request.Host}";
        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = request.Amount * 100, // Stripe expects amount in cents
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Payment for School Services"
                        }
                    },
                    Quantity = 1
                }
            },
            Mode = "payment",
            SuccessUrl = $"{domain}/success",
            CancelUrl = $"{domain}/cancel"
        };

        var service = new SessionService();
        Session session = service.Create(options);

        return Ok(new { id = session.Id });
    }
}

public class PaymentRequest
{
    public int Amount { get; set; }
}

