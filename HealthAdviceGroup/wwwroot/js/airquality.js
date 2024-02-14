// Array to store pollution conditions
let conditions = ["Good", "Fair", "Moderate", "Poor", "Very Poor"];

// Object to store pollutant ranges
let pollutantRanges = {
    "SO2": {
        1: [0, 20],
        2: [20, 80],
        3: [80, 250],
        4: [250, 350],
        5: [350, Infinity]
    },
    "NO2": {
        1: [0, 40],
        2: [40, 70],
        3: [70, 150],
        4: [150, 200],
        5: [200, Infinity]
    },
    "PM10": {
        1: [0, 20],
        2: [20, 50],
        3: [50, 100],
        4: [100, 200],
        5: [200, Infinity]
    },
    "PM2.5": {
        1: [0, 10],
        2: [10, 25],
        3: [25, 50],
        4: [50, 75],
        5: [75, Infinity]
    },
    "O3": {
        1: [0, 60],
        2: [60, 100],
        3: [100, 140],
        4: [140, 180],
        5: [180, Infinity]
    },
    "CO": {
        1: [0, 4400],
        2: [4400, 9400],
        3: [9400, 12400],
        4: [12400, 15400],
        5: [15400, Infinity]
    }
};

// Function to get current location and fetch air pollution data
navigator.geolocation.getCurrentPosition(function (position) {
    // Extract latitude and longitude
    latitude = position.coords.latitude;
    longitude = position.coords.longitude;

    // AJAX call to retrieve air pollution data
    $.ajax({
        type: "POST",
        url: "http://localhost:5074/Dashboard/GetOWMData",
        data: { latitude: latitude, longitude: longitude, option: "air_pollution" },

        // Success callback function to update pollution HUD
        success: function (data) {
            updatePollutionHUD(data);
        },

        // Error callback function to handle errors
        error: function (error) {
            console.log(error);
            alert("You need to allow location services or enter a location to use this feature.")
        }
    });
});

// Function to calculate Air Quality Index (AQI) based on pollutant concentration
function calculateAQI(pollutant, concentration) {
    for (let i = 1; i <= 5; i++) {
        if (concentration >= pollutantRanges[pollutant][i][0] && concentration < pollutantRanges[pollutant][i][1]) {
            return conditions[i - 1];
        }
    }
    return "Error";
}

// Function to update pollution HUD elements with data
function updatePollutionHUD(data) {
    const parsedData = JSON.parse(data);
    const components = parsedData.list[0].components;

    // Object to store pollutant elements and their corresponding values
    const pollutantElements = {
        ".co-text": components.co,
        ".no2-text": components.no2,
        ".o3-text": components.o3,
        ".pm2_5-text": components.pm2_5,
        ".pm10-text": components.pm10,
        ".so4-text": components.so2,
        ".aqi-text": parsedData.list[0].main.aqi
    };

    // Update pollutant elements with values
    for (const [selector, value] of Object.entries(pollutantElements)) {
        updateTextContent(selector, value);
    }

    // Object to store AQI elements and their corresponding values
    const aqiElements = {
        ".co-aqi": calculateAQI("CO", components.co),
        ".no2-aqi": calculateAQI("NO2", components.no2),
        ".o3-aqi": calculateAQI("O3", components.o3),
        ".pm2_5-aqi": calculateAQI("PM2.5", components.pm2_5),
        ".pm10-aqi": calculateAQI("PM10", components.pm10),
        ".so4-aqi": calculateAQI("SO2", components.so2)
    };

    // Update AQI elements with values
    for (const [selector, value] of Object.entries(aqiElements)) {
        updateTextContent(selector, value);
    }
}
