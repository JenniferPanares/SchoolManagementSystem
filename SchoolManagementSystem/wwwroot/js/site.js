// Google Maps Initialization
function initMap() {
    const schoolLocation = { lat: 52.2682, lng: -113.8113 }; // Coordinates for Red Deer Polytechnic
    const map = new google.maps.Map(document.getElementById("map"), {
        zoom: 15,
        center: schoolLocation,
    });
    new google.maps.Marker({
        position: schoolLocation,
        map: map,
    });
}

// Weather API Integration
function fetchWeather(apiKey) {
    fetch(`https://api.openweathermap.org/data/2.5/weather?q=Red+Deer,CA&appid=${apiKey}&units=metric`) // Replace _PUBLIC_KEY with your Stripe public API key
        .then(response => response.json())
        .then(data => {
            document.getElementById("weather").innerHTML =
                `Current temperature in Red Deer: ${data.main.temp}°C, ${data.weather[0].description}`;
        })
        .catch(error => console.error('Error fetching weather data:', error));
}

document.addEventListener("DOMContentLoaded", () => {
    // Initialize Google Maps if the map element is present
    if (document.getElementById("map")) {
        initMap();
    }
});

// Stripe Payment Integration
function handleStripePayment(amount) {
    var stripe = Stripe('YOUR_STRIPE_PUBLIC_KEY'); // Replace YOUR_STRIPE_PUBLIC_KEY with your Stripe public API key
    stripe.redirectToCheckout({
        lineItems: [{ price: 'PRICE_ID', quantity: 1 }],
        mode: 'payment',
        successUrl: window.location.origin + '/success',
        cancelUrl: window.location.origin + '/cancel',
    }).then(function (result) {
        if (result.error) {
            alert(result.error.message);
        }
    });
}

// FluentValidation Integration
function validateForm() {
    const firstName = document.getElementById("FirstName").value;
    const lastName = document.getElementById("LastName").value;
    const email = document.getElementById("Email").value;
    const password = document.getElementById("Password").value;
    const confirmPassword = document.getElementById("ConfirmPassword").value;

    if (!firstName) {
        alert("First name is required");
        return false;
    }
    if (!lastName) {
        alert("Last name is required");
        return false;
    }
    if (!email.includes("@")) {
        alert("A valid email is required");
        return false;
    }
    if (password.length < 6) {
        alert("Password must be at least 6 characters long");
        return false;
    }
    if (password !== confirmPassword) {
        alert("Passwords must match");
        return false;
    }
    return true;
}

// Attach validateForm to form submit event
const form = document.getElementById("registrationForm");
if (form) {
    form.addEventListener("submit", function (event) {
        if (!validateForm()) {
            event.preventDefault();
        }
    });
}
