using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITRoots.BL.Models
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Email Is Required")]
        [MinLength(5, ErrorMessage = "MinLength password is 5")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword Is Required")]
        [Compare("Password",ErrorMessage = "Confirm Password Does Not Match")]
        public string? ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }
    }
}
