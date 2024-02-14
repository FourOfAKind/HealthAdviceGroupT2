// Global variables to store latitude and longitude
let latitude;
let longitude;

// Function to convert temperature from Kelvin to Celsius
function toCelsius(kelvin) {
    kelvin = parseFloat(kelvin);
    return Math.round(kelvin - 273.15);
}
// Function to convert timestamp to time format (HH:MM)
function toTime(timestamp) {
    let date = new Date(timestamp * 1000); // Convert timestamp to Date object
    let hours = date.getHours().toString().padStart(2, "0"); // Get hours with added zero if theres only one digit
    let minutes = date.getMinutes().toString().padStart(2, "0"); // Get minutes with added zero if theres only one digit
    return `${hours}:${minutes}`;
}

// Function to toggle between light and dark themes
function setTheme() {
    var currentTheme = localStorage.getItem('theme');
    if (currentTheme === 'light') {
        localStorage.setItem('theme', 'dark'); 
    } else {
        localStorage.setItem('theme', 'light');
    }
    applyTheme();
}

// Function to apply the theme stored in local storage
function applyTheme() {
    var theme = localStorage.getItem('theme'); 
    if (theme === 'light') {
        document.documentElement.setAttribute('data-bs-theme', 'light'); 
    } else if (theme === 'dark') {
        document.documentElement.setAttribute('data-bs-theme', 'dark');
    }
}

// Function to update text content of an element with a given selector
function updateTextContent(selector, value) {
    const element = document.querySelector(selector); 
    if (element) {
        element.textContent = value; 
    }
}
