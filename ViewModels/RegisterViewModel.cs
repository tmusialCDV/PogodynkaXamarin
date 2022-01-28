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
    public class RegisterViewModel : INotifyPropertyChanged
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string RegisterInfo { get; set; }
        public string RegisterInfoColor { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private ApiServices apiService = new ApiServices();
        public ICommand RegisterCommand => new Command(RegisterUser);

        async void RegisterUser()
        {
            RegisterInfoColor = "Red";
            NotifyPropertyChanged("RegisterInfoColor");

            if (!Email.Contains("@") || Email.Length < 5)
            {
                RegisterInfo = "Podano niepoprawny email.";
                NotifyPropertyChanged("RegisterInfo");
                return;
            }
            if (Password.Length <= 5)
            {
                RegisterInfo = "Hasło musi zawierać minimum 6 znaków.";
                NotifyPropertyChanged("RegisterInfo");
                return;
            }
            if (Password != ConfirmPassword)
            {
                RegisterInfo = "Hasła się nie zgadzają. Spróbuj ponownie.";
                NotifyPropertyChanged("RegisterInfo");
                return;
            }
            else
            {
                RegisterInfo = "Poczekaj. Trwa rejestracja.";
                NotifyPropertyChanged("RegisterInfo");
            }

            try
            {
                await apiService.RegisterAsync(Email, Password, ConfirmPassword);
            }
            catch (FlurlHttpException ex)
            {
                if (ex.StatusCode == 400)
                {
                    RegisterInfo = "Ten email jest już zajęty.";
                    NotifyPropertyChanged("RegisterInfo");
                    return;
                }
                else
                {
                    RegisterInfo = "Wystąpił błąd. Spróbuj ponownie później.";
                    NotifyPropertyChanged("RegisterInfo");
                    return;
                }
            }
            RegisterInfoColor = "Green";
            NotifyPropertyChanged("RegisterInfoColor");

            RegisterInfo = "Pomyślnie zarejestrowano konto. Możesz się teraz zalogować.";
            NotifyPropertyChanged("RegisterInfo");
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
