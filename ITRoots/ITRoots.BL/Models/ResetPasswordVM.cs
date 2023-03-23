using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITRoots.BL.Models
{
    public class ResetPasswordVM
    {
        [Required(ErrorMessage = "Password is required")]
        [MinLength(5, ErrorMessage = "Minimum Password Length is 5")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [MinLength(5, ErrorMessage = "Minimum Confirm Password Length is 5")]
        [Compare("Password", ErrorMessage = "Password doesn't Match")]
        public string? ConfirmPassword { get; set; }

        public string? Email { get; set; }
        public string? Token { get; set; }
    }
}
