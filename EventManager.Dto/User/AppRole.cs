using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Dto.User
{
    public class AppRole
    {
        [Key]
        public Guid RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }
}
