using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Dto.User
{
    public class AppUser
    {
        public AppUser() { }
        [Key]
        public Guid UniqueId { get; set; }
        [Required]
        public string Email { get; set; } = string.Empty;

        [NotMapped]
        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime LastModifiedDate { get; set; } = DateTime.Now;
        public DateTime LastLoggedIn { get; set; } = DateTime.Now;
        public string ?Roles { get; set; } = string.Empty;

        public AppUser(UserRegistrationVM _userVm)
        {
            Email = _userVm.Email;
            Password = _userVm.Password;
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(_userVm.Password);
        }
    }
}
