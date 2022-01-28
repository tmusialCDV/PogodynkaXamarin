using Pogodynka.Models;
using Pogodynka.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Pogodynka.ViewModels
{
    internal class WeatherViewModel
    {
        public WeatherViewModel(WeatherModel model)
        {
            DataTransfer(model);
        }

        public string InsertedCity { get; set; }
        
        public int Temp { get; set; }
        public int TempFeelsLike { get; set; }
        public int MinTemp { get; set; }
        public int MaxTemp { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public string Description { get; set; }
        public ImageSource ImageSource { get; set; }
        public ICommand AddToDatabase => new Command(addToDatabase);

        void DataTransfer(WeatherModel model)
        {
            ImageSource = ImageSource.FromUri(new Uri($"http://openweathermap.org/img/wn/{model.Weather[0].Icon}@2x.png"));
            InsertedCity = model.Name;
            Temp = (int)Math.Round(model.Main.Temp, MidpointRounding.ToEven);
            TempFeelsLike = (int)Math.Round(model.Main.Feels_Like, MidpointRounding.ToEven);
            MinTemp = (int)Math.Round(model.Main.Temp_Min);
            MaxTemp = (int)Math.Round(model.Main.Temp_Max);
            Pressure = (int)model.Main.Pressure;
            Humidity = (int)model.Main.Humidity;
            Description = model.Weather[0].Description;
        }

        async void addToDatabase()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AddToDatabasePage(Temp));
        }
    }
}
