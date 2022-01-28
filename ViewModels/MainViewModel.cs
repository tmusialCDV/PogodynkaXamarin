using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using Pogodynka.Services;
using System.ComponentModel;
using System.Threading;
using Pogodynka.Models;
using Flurl.Http;

namespace Pogodynka.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public string CityEntry { get; set; }
        public string State { get; set; }
        public ICommand Logout => new Command(logoutUser);
        public ICommand GetDatabase => new Command(getDatabase);
        public ICommand CityButton => new Command(CityButtonClicked);
        public ICommand GpsButton => new Command(GpsButtonClicked);
        private WeatherApiServices weatherApiServices = new WeatherApiServices();
        private ApiServices apiService = new ApiServices();
        private readonly Page page;
        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel(Page page)
        {
            this.page = page;
        }

        void logoutUser()
        {
            SecureStorage.RemoveAll();
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }

        async void CityButtonClicked()
        {
            if(CityEntry == null)
            {
                await page.DisplayAlert("Alert", "Pole miasta nie może być puste.", "OK");
                return;
            }

            State = "Ładowanie pogody.";
            NotifyPropertyChanged("State");

            try
            {
                var weather = await weatherApiServices.GetWeatherForCity(CityEntry);
                await Application.Current.MainPage.Navigation.PushAsync(new WeatherPage(weather));
                State = "";
                NotifyPropertyChanged("State");
            }
            catch (FlurlHttpException ex)
            {
                await page.DisplayAlert("Alert", "Nie znaleziono podanej miejscowości.", "OK");
                State = "";
                NotifyPropertyChanged("State");
                return;
            }

            
        }

        async void GpsButtonClicked()
        {
            State = "Pobieranie lokalizacji.";
            NotifyPropertyChanged("State");

            var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            var cts = new CancellationTokenSource();
            var location = await Geolocation.GetLocationAsync(request, cts.Token);

            State = "Ładowanie pogody.";
            NotifyPropertyChanged("State");
            var weather = await weatherApiServices.GetWeatherForGps(location);
            await Application.Current.MainPage.Navigation.PushAsync(new WeatherPage(weather));
            State = "";
            NotifyPropertyChanged("State");
        }

        async void getDatabase()
        {
            State = "Ładowanie postów...";
            NotifyPropertyChanged("State");
            try
            {
                var posts = await apiService.GetPosts();
                State = "";
                NotifyPropertyChanged("State");
                await Application.Current.MainPage.Navigation.PushAsync(new GetDatabasePage(posts));
            }
            catch(FlurlHttpException ex)
            {
                State = "";
                NotifyPropertyChanged("State");
                await page.DisplayAlert("Warning", "Błąd. Spróbuj ponownie później.", "OK");
                return;
            }
            
        }

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
