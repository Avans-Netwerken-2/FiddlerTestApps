using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Common;

namespace APIClient
{
    class Program
    {
        private const string RequestUri = "http://localhost:5107/weatherforecast";

        static async Task Main(string[] args)
        {
            Console.WriteLine("When ready press <ENTER>");
            Console.ReadLine();
            var client = new HttpClient();

            var result = await client.GetAsync(RequestUri);


            var newForeCast = new WeatherForecast
            {
                Timestamp = DateTime.Now.AddMinutes(-5),
                Summary = "Some summary",
                TemperatureC = 24
            };

            var request = JsonSerializer.Serialize(newForeCast);

            var response = await client.PostAsync(RequestUri,
                new StringContent(request, Encoding.UTF8, "application/json"));

            Console.WriteLine(response);
            Console.WriteLine("Done.\r\nPress <ENTER> to exit");
            Console.ReadLine();
        }
    }
}
