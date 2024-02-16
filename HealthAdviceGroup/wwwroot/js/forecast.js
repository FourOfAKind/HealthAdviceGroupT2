// Function to get current location and fetch weather data
navigator.geolocation.getCurrentPosition(function (position) {
    // Extract latitude and longitude from position object
    latitude = position.coords.latitude;
    longitude = position.coords.longitude;

    // AJAX call to retrieve weather data
    $.ajax({
        type: "POST",
        url: "http://localhost:5074/Dashboard/GetOWMData",
        data: { latitude: latitude, longitude: longitude, option: "weather" },

        // Success callback function to update weather HUD
        success: function (data) {
            updateWeatherHUD(data);
        },

        // Error callback function to handle errors
        error: function (error) {
            console.log(error);
            alert("You need to allow location services or enter a location to use this feature.")
        }
    });
});

// Function to update weather HUD elements with data
function updateWeatherHUD(data) {
    // Parse JSON data received from AJAX call
    const parsedData = JSON.parse(data);
    console.log(parsedData);

    // Update weather HUD elements with corresponding data
    updateTextContent(".location-text", parsedData.name);
    document.querySelector(".icon-img").src = `http://openweathermap.org/img/w/${parsedData.weather[0].icon}.png`
    updateTextContent(".condition-text", parsedData.weather[0].main);
    updateTextContent(".temperature-text", toCelsius(parsedData.main.temp));
    updateTextContent(".lastupdated-text", toTime(parsedData.dt));
    updateTextContent(".humidity-text", parsedData.main.humidity);
    updateTextContent(".airpressure-text", parsedData.main.pressure);
    updateTextContent(".sunrise-text", toTime(parsedData.sys.sunrise));
    updateTextContent(".sunset-text", toTime(parsedData.sys.sunset));
}
