using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Api
{
    public static class GetWeather
    {
        [FunctionName("GetWeather")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "weather")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Api.weather.json");
            using var reader = new StreamReader(stream);
            var json = await reader.ReadToEndAsync();
            var forecasts = JsonConvert.DeserializeObject<WeatherForecast[]>(json);

            return new OkObjectResult(forecasts);
        }
    }
}

