using Pogodynka.Models;
using Pogodynka.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Pogodynka.ViewModels
{
    public class GetDatabaseViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ShowPostModel> PostItems { get; set; }
        public ICommand deletePost => new Command(DeletePost);
        private ApiServices apiService = new ApiServices();
        private ObservableCollection<GetPostModel> postsAfterDelete;
        private readonly Page page;
        public event PropertyChangedEventHandler PropertyChanged;

        public GetDatabaseViewModel(ObservableCollection<GetPostModel> posts, Page page)
        {
            PostItems = new ObservableCollection<ShowPostModel>();
            this.page = page;

            foreach (var post in posts)
            {
                if (post.imageData != null)
                {
                    MemoryStream imageStream = new MemoryStream(post.imageData);

                    PostItems.Add(new ShowPostModel
                    {
                        id = post.id,
                        tempC = post.tempC,
                        tempF = post.tempF,
                        dateTime = post.dateTime,
                        description = post.description,
                        imageData = ImageSource.FromStream(() => imageStream)
                    });
                }
            }

            
        }
        async void DeletePost(object o)
        {
            ShowPostModel post = (ShowPostModel)o;
            bool asnwer = await page.DisplayAlert("Alert", "Czy na pewno chcesz usunąć wpis?", "Tak", "Nie");
            if (asnwer)
            {
                await apiService.DeletePost(post.id);
                await ViewPostsAfterDelete();
                NotifyPropertyChanged("PostItems");
            }
            else return;
        }

        async Task ViewPostsAfterDelete()
        {
            postsAfterDelete = await apiService.GetPosts();
            PostItems = new ObservableCollection<ShowPostModel>();
            foreach (var post in postsAfterDelete)
            {
                if (post.imageData != null)
                {
                    MemoryStream imageStream = new MemoryStream(post.imageData);

                    PostItems.Add(new ShowPostModel
                    {
                        id = post.id,
                        tempC = post.tempC,
                        tempF = post.tempF,
                        dateTime = post.dateTime,
                        description = post.description,
                        imageData = ImageSource.FromStream(() => imageStream)
                    });
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
