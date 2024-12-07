using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

[ApiController]
[Route("api/config")]
public class ConfigController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public ConfigController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("get-google-maps-key")]
    public IActionResult GetGoogleMapsKey()
    {
        var apiKey = _configuration["GoogleMaps"];
        if (string.IsNullOrEmpty(apiKey))
        {
            return BadRequest("Google Maps API key not found.");
        }

        return Ok(new { apiKey });
    }

    [HttpGet("get-weather-key")]
    public IActionResult GetWeatherKey()
    {
        var apiKey = _configuration["OpenWeatherMap"];
        if (string.IsNullOrEmpty(apiKey))
        {
            return BadRequest("OpenWeatherMap API key not found.");
        }

        return Ok(new { apiKey });
    }

    [HttpGet("get-stripe-publishable-key")]
    public IActionResult GetStripePublishableKey()
    {
        var publishableKey = _configuration["StripePublishableKey"];
        if (string.IsNullOrEmpty(publishableKey))
        {
            return BadRequest("Stripe Publishable Key not found.");
        }

        return Ok(new { publishableKey });
    }

    // The secret key should only be used in backend and never exposed to the frontend.
    [HttpGet("get-stripe-secret-key")]
    public IActionResult GetStripeSecretKey()
    {
        return Unauthorized("Stripe Secret Key cannot be exposed in the frontend.");
    }
}
