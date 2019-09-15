using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicApp.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Input Username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Input Password")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
