using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Pogodynka.Services;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Flurl.Http;

namespace Pogodynka.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private ApiServices _apiService = new ApiServices();

        public string Email { get; set; }
        public string Password { get; set; }
        public string LoginInfo { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand LoginCommand => new Command(LoginUser);

        

        async void LoginUser()
        {
            if (!Email.Contains("@")||Email.Length<5)
            {
                LoginInfo = "Podano niepoprawny email.";
                NotifyPropertyChanged("LoginInfo");
                return;
            }
            if(Password.Length <= 5)
            {
                LoginInfo = "Hasło musi zawierać minimum 6 znaków.";
                NotifyPropertyChanged("LoginInfo");
                return;
            }
            else
            {
                LoginInfo = "Poczekaj. Trwa logowanie.";
                NotifyPropertyChanged("LoginInfo");
            }

            try
            {
                await _apiService.LoginAsync(Email, Password);
                Application.Current.MainPage = new NavigationPage(new MainPage());
            }
            catch (FlurlHttpException ex)
            {
                if(ex.StatusCode == 400)
                {
                    LoginInfo = "Podano niepoprawny email lub hasło.";
                    NotifyPropertyChanged("LoginInfo");
                }
                else
                {
                    LoginInfo = "Wystąpił błąd. Spróbuj ponownie później.";
                    NotifyPropertyChanged("LoginInfo");
                }
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
