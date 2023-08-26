using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace EventManager.Dto.User
{
    public class UserRegistrationVM
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(4)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        public string OrganizationName { get; set; } = string.Empty;

        public string ErrorMessage { get; set; } = string.Empty;
        public string SuccessMessage { get; set; } = string.Empty;
        public string NotificationMessage { get; set; } = string.Empty;

    }
}
