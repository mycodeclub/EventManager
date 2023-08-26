using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Dto.User
{
    public class LoginResponse
    {
        public bool IsLoginSuccess { get; set; }
        public string Token { get; set; } = string.Empty;
        public string EpOrgId { get; set; } = string.Empty;
        public List<string>? ErrorMessages { get; set; }
    }
}
