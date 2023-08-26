using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EventManager.Dto.User
{
    public class LoginVM
    {
        [Required]
        [Display(Name = "Login Name")]
        public string LoginName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
