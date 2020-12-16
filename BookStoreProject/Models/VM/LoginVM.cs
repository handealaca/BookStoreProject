using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Models.VM
{
    public class LoginVM
    {
        [Required(ErrorMessage = "EMail field is required. ")]
        public string EMail { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        [Required(ErrorMessage = "Password field is required.")]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
    }
}
