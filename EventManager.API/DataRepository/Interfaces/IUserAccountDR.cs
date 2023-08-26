using EventManager.Dto;
using EventManager.Dto.User;

namespace EventManager.API.DataRepository.Interfaces
{
    public interface IUserAccountDR
    {

        public Task<ApiResponse> RegisterNewUser(UserRegistrationVM userVM, string? roles);
        public Task<LoginResponse> Login(string LoginName, string Password);
        public Task<bool> IfUserExists(string email);
        public Task<IEnumerable<Dto.User.AppUser>> GetAllUsers();


    }
}
