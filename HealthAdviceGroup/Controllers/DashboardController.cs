using HealthAdviceGroup.Data;
using HealthAdviceGroup.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace HealthAdviceGroup.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly ApplicationDbContext _context;

        public DashboardController(ILogger<DashboardController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // POST: Dashboard/GetOWMData
        [HttpPost]
        public async Task<ActionResult> GetOWMData(double latitude, double longitude, string option)
        {
            // Using HttpClient to interact with the OpenWeatherMap API
            using (var client = new HttpClient())
            {
                // Set the base address for the OpenWeatherMap API
                client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/");
                var response = new HttpResponseMessage();

                // Check the chosen option to determine the type of API call
                if (option.ToLower() == "weather")
                {
                    // Make a request to retrieve current weather data
                    response = await client.GetAsync($"weather?lat={latitude}&lon={longitude}&appid={Environment.GetEnvironmentVariable("API_KEY")}");
                    if (response.IsSuccessStatusCode)
                    {
                        // Return the weather data if successful
                        return Ok(await response.Content.ReadAsStringAsync());
                    }
                    else
                    {
                        // Return an error message if the request is not successful
                        return Json("Error");
                    }
                }
                else if (option.ToLower() == "air_pollution")
                {
                    // Make a request to retrieve air pollution data
                    response = await client.GetAsync($"air_pollution?lat={latitude}&lon={longitude}&appid={Environment.GetEnvironmentVariable("API_KEY")}");
                    if (response.IsSuccessStatusCode)
                    {
                        // Return the air pollution data if successful
                        return Ok(await response.Content.ReadAsStringAsync());
                    }
                    else
                    {
                        // Return an error message if the request is not successful
                        return Json("Error");
                    }
                }
                else
                {
                    // Return "Null" if the option is not recognized
                    return Json("Null");
                }
            }
        }

        // GET: Dashboard/Forecast
        public IActionResult Forecast()
        {
            return View();
        }

        // GET: Dashboard/AirQuality
        public IActionResult AirQuality()
        {
            return View();
        }

        // GET: Dashboard/Privacy
        public IActionResult Privacy()
        {
            return View();
        }

        // GET: Dashboard/Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Return the Error view with the request identifier
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
