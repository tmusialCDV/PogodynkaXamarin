using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pogodynka
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected async override void OnStart()
        {
            if(await SecureStorage.GetAsync("jwt") != null)
            {
                MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
