using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace Pogodynka.Models
{
    public class ShowPostModel
    {
        public int id { get; set; }
        public int tempC { get; set; }
        public int tempF { get; set; }
        public DateTime dateTime { get; set; }
        public string description { get; set; }
        public ImageSource imageData { get; set; }
    }
}
