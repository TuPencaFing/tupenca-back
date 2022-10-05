using Microsoft.AspNetCore.Mvc;
using tupenca_back.Model;
using tupenca_back.Services;

namespace tupenca_back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<ServiceController> _logger;
        private readonly UserService _userService;
        public ServiceController(ILogger<ServiceController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public IActionResult Create(User usr)
        {
            _userService.addUser(usr);
            return Ok(new { message = "User added" });
        }
    }
}