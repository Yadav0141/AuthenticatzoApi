using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Authenticatzo.Models
{
   public class LoginModel
    {
        [Required(ErrorMessage = "Username is requried.")]
        public string username { get; set; }
        [Required(ErrorMessage = "Password is requried.")]
        public string password { get; set; }
    }
}
