using BookStoreProject.Models.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Models.VM
{
    public class AdminUserVM
    {
        public int UserID { get; set; }
        [Required(ErrorMessage = "Name field is required.")]

        public string Name { get; set; }

        [Required(ErrorMessage = "SurName field is required.")]
        public string SurName { get; set; }
    
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]

        [Required(ErrorMessage = "Email field is required.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password field is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password field is required.")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
      
        public string ConfirmPassword { get; set; }

        
       

        public DateTime LoginDate { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; }

        public List<string> rolelist { get; set; }

        public string role { get; set; }

        public List<EnumRole> roles { get; set; }
        public List<EnumRole> selectedroles { get; set; }
    }
}
