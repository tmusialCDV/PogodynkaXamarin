using Pogodynka.Models;
using Pogodynka.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pogodynka
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GetDatabasePage : ContentPage
    {
        public GetDatabasePage(ObservableCollection<GetPostModel> posts)
        {
            InitializeComponent();
            BindingContext = new GetDatabaseViewModel(posts, this);
        }
    }
}