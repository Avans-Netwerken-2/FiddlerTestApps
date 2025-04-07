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
        

        static async Task Main(string[] args)
        {
            string requestUriStr = "http://localhost:5107/weatherforecast";
            Uri? uriResult = null;
            if (args.Length > 0)
            {
                requestUriStr = args[0];
                var res = Uri.TryCreate(requestUriStr, UriKind.Absolute, out uriResult);
            }
            Console.WriteLine("When ready press <ENTER>");
            Console.ReadLine();
            var client = new HttpClient();

            var result = await client.GetAsync(uriResult);


            var newForeCast = new WeatherForecast
            {
                Timestamp = DateTime.Now.AddMinutes(-5),
                Summary = "Some summary",
                TemperatureC = 24
            };

            var request = JsonSerializer.Serialize(newForeCast);

            var response = await client.PostAsync(requestUriStr,
                new StringContent(request, Encoding.UTF8, "application/json"));

            Console.WriteLine(response);
            Console.WriteLine("Done.\r\nPress <ENTER> to exit");
            Console.ReadLine();
        }
    }
}
