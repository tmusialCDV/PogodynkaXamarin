using System;
using System.Collections.Generic;
using System.Text;

namespace Pogodynka.Models
{
    internal class PostModel
    {
        public int tempC { get; set; }
        public DateTime dateTime = DateTime.Now;
        public string description { get; set; }
        public byte[] imageData { get; set; }

        public PostModel(int tempC, string description, byte[] imageData)
        {
            this.tempC = tempC;
            this.description = description;
            this.imageData = imageData;
        }
    }
}
