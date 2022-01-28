using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Flurl.Http;
using Newtonsoft.Json;
using Pogodynka.Models;
using Xamarin.Essentials;

namespace Pogodynka.Services
{
    internal class WeatherApiServices
    {
        public async Task<WeatherModel> GetWeatherForCity(string city)
        {
            var weather = await $"https://api.openweathermap.org/data/2.5/weather?appid=1b213cbdd2f4fdc350606666f558fca4&units=metric&lang=pl&q={city}"
                .GetAsync()
                .ReceiveJson<WeatherModel>();
            return weather;
        }

        public async Task<WeatherModel> GetWeatherForGps(Location location)
        {
            var weather = await $"http://api.openweathermap.org/data/2.5/weather?appid=1b213cbdd2f4fdc350606666f558fca4&units=metric&lang=pl&lat={location.Latitude}&lon={location.Longitude}"
                .GetAsync()
                .ReceiveJson<WeatherModel>();
            return weather;
        }
    }
}
