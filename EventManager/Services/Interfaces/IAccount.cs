using EventManager.Dto;
using EventManager.Dto.User;

namespace EventManager.Web.Services.Interfaces
{
    public interface IAccount
    {
        public Task<ApiResponse> SignUp(UserRegistrationVM userRegistrationRequest);
        public Task<ApiResponse> EventOrgSignUp(UserRegistrationVM userRegistrationRequest);
        public Task<LoginResponse> Login(LoginVM loginRequest);
        public Task<IEnumerable<AppUser>> GetAllUsers();
    }
}
