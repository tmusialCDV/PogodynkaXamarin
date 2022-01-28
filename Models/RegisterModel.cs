using System;
using System.Collections.Generic;
using System.Text;

namespace Pogodynka.Models
{
    public class RegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public RegisterModel(string Email, string Password, string ConfirmPassword)
        {
            this.Email = Email;
            this.Password = Password;
            this.ConfirmPassword = ConfirmPassword;
        }

        
    }
}
