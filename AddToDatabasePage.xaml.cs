﻿using Pogodynka.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pogodynka
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddToDatabasePage : ContentPage
    {
        public AddToDatabasePage(int Temp)
        {
            InitializeComponent();
            BindingContext = new AddToDatabaseViewModel(Temp, this);
        }
    }
}