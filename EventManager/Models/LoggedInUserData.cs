using System.Security.Claims;

namespace EventManager.Web.Models
{
    public class LoggedInUserData
    {
        private HttpContext _httpContext;

        public string LoggedInUserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public Guid LoggedInUserId { get; set; }
        public Guid LoggedInEpOrgId { get; set; }


        public LoggedInUserData(HttpContext httpContext)
        {
            _httpContext = httpContext;
            if (httpContext.User != null)
            {

                LoggedInUserName = _httpContext.User.Identity.Name;
                Email = _httpContext.User.FindFirstValue(ClaimTypes.Email);
                Role = _httpContext.User.FindFirstValue(ClaimTypes.Role);
                Guid.TryParse(_httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid LoggedInUserId);
                Guid.TryParse(_httpContext.User.FindFirstValue(ClaimTypes.Sid), out Guid LoggedInEpOrgId);
            }
        }
    }
}
