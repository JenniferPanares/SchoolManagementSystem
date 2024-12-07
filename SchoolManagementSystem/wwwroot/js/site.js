// Google Maps Initialization
function initMap() {
    const redDeerLocation = { lat: 52.2681, lng: -113.8112 }; // Red Deer Polytechnic coordinates
    const map = new google.maps.Map(document.getElementById("map"), {
        zoom: 15,
        center: redDeerLocation,
    });
    const marker = new google.maps.Marker({
        position: redDeerLocation,
        map: map,
        title: "Red Deer Polytechnic",
    });
}


// Weather API Integration
async function fetchWeather(apiKey) {
    const city = "Red Deer"; // Location to get weather for
    const url = `https://api.openweathermap.org/data/2.5/weather?q=${city}&appid=${apiKey}&units=metric`;

    try {
        const response = await fetch(url);
        if (!response.ok) {
            throw new Error("Failed to fetch weather data.");
        }
        const weatherData = await response.json();

        const weatherDiv = document.getElementById("weather");
        weatherDiv.innerHTML = `
            <h3>Current Weather</h3>
            <p>Location: ${weatherData.name}</p>
            <p>Temperature: ${weatherData.main.temp} °C</p>
            <p>Weather: ${weatherData.weather[0].description}</p>
        `;
    } catch (error) {
        console.error("Error fetching weather data:", error);
    }
}


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
