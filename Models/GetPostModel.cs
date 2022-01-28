using System;
using System.Collections.Generic;
using System.Text;

namespace Pogodynka.Models
{
    public class GetPostModel
    {
        public int id { get; set; }
        public int tempC { get; set; }
        public int tempF { get; set; }
        public DateTime dateTime { get; set; }
        public string description { get; set; }
        public byte[] imageData { get; set; }
    }
}
