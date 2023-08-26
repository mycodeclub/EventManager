using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Dto.User
{
    [Obsolete("Remove it")]
    public class UserRole
    {
        [Key]
        public Guid UniqueId { get; set; }

        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        [ForeignKey("UserId")]
        public AppUser? User { get; set; }
        [ForeignKey("RoleId")]
        public AppRole? Role { get; set; }
    }
}
