using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITRoots.BL.Models
{
    public class LoginVM
    {

        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "password Is Required")]
        [MinLength(5, ErrorMessage = "MinLength password is 5")]
        public string? Password { get; set; }

        
        public bool RememberMe { get; set; }
    }
}
