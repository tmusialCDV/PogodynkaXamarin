using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Pogodynka.Services;
using static System.Net.WebRequestMethods;
using Flurl.Http;

namespace Pogodynka.ViewModels
{
    internal class AddToDatabaseViewModel: INotifyPropertyChanged
    {
        public int TempC { get; set; }
        public string Description { get; set; }
        public string PhotoState { get; set; }
        public ICommand TakePicture => new Command(takePicture);
        public ICommand AddToDatabase => new Command(addToDatabase);
        public ImageSource ImageSource { get; set; }
        public Stream PhotoStream { get; set; }
        public FileResult PhotoResult { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ApiServices apiService = new ApiServices();
        private readonly Page page;

        public AddToDatabaseViewModel(int Temp, Page page)
        {
            TempC = Temp;
            this.page = page;
        }

        async void takePicture()
        {
            PhotoResult = await MediaPicker.CapturePhotoAsync();
            PhotoStream = await PhotoResult.OpenReadAsync();
            ImageSource = ImageSource.FromStream(() => PhotoStream);
            PhotoState = "Wybrano zdjęcie:";
            NotifyPropertyChanged("ImageSource");
            NotifyPropertyChanged("PhotoState");
        }

        async void addToDatabase()
        {
            if(PhotoResult == null)
            {
                await page.DisplayAlert("Alert", "Nie zrobiono żadnego zdjęcia.", "OK");
                return;
            }
            await apiService.AddPost(TempC, Description, PhotoResult);
            await page.DisplayAlert("Alert", "Pomyślnie dodano wpis do dziennika.", "OK");
            await Application.Current.MainPage.Navigation.PopAsync();
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
