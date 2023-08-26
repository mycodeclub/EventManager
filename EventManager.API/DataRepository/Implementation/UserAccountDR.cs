using EventManager.API.DataRepository.Interfaces;
using EventManager.API.EfData;
using EventManager.Dto;
using EventManager.Dto.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventManager.API.DataRepository.Implementation
{
    public class UserAccountDR : IUserAccountDR
    {
        private AppDbContext _context;
        private IConfiguration _config;


        public UserAccountDR(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;

        }

        public async Task<ApiResponse> RegisterNewUser(UserRegistrationVM userVM, string roles)
        {
            var response = new ApiResponse();

            if (await IfUserExists(userVM.Email))
                response.ErrorMessages = new List<string>() { "User already exist with given Id, please try with different email" };
            else
            {
                var user = new AppUser()
                {
                    UniqueId = Guid.NewGuid(),
                    Email = userVM.Email,
                    CreatedDate = DateTime.UtcNow,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(userVM.Password),
                    Roles = roles
                };
                _context.AppUsers.Add(user);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
                response.UniqueId = user.UniqueId;

                //if (roles != null && roles.Count() > 0)
                //    await AddUserRoles(user.UniqueId, roles);
                response.IsCreated = true;
            }
            return response;
        }
        public async Task<bool> IfUserExists(string email)
        {
            return await _context.AppUsers.AnyAsync(u => u.Email.Equals(email));
        }
        public async Task<LoginResponse> Login(string LoginName, string Password)
        {
            var response = new LoginResponse() { };
            if (await ValidateCredentials(LoginName, Password))
            {
                response.Token = await CreateJwtToken(LoginName);
                response.IsLoginSuccess = true;
                response.EpOrgId = await GetEpOrgIdByLoginEmail(LoginName);
            }
            else response.ErrorMessages = new List<string>() { "Invalid Credentials " };
            return response;

        }

        public async Task<IEnumerable<Dto.User.AppUser>> GetAllUsers()
        {
            IEnumerable<Dto.User.AppUser> users;
            users = await _context.AppUsers.ToListAsync();
            return users;
        }

        private async Task<bool> ValidateCredentials(string email, string password)
        {
            var _appUser = await GetUserByEmail(email);
            return _appUser == null ? false
                : BCrypt.Net.BCrypt.Verify(password, _appUser.PasswordHash);
        }
        private async Task<AppUser> GetUserByEmail(string email)
        {
            AppUser appUser = null;
            try
            {
                var result = await _context.AppUsers.Where(u => u.Email.Equals(email)).FirstOrDefaultAsync();
                appUser = result;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;

            }
            return appUser;


        }

        private async Task<string> GetEpOrgIdByLoginEmail(string email)
        {
            var _query = from epo in _context.EventPlanners
                         join user in _context.AppUsers on epo.AppUserId equals user.UniqueId
                         where user.Email == email
                         select new { epo.UniqueId };
            var _result = await _query.FirstOrDefaultAsync();
            return _result.UniqueId.ToString();
        }
        private async Task<string> CreateJwtToken(string email)
        {

            var secretKey = _config["JwtSettings:Key"];
            var validIssuer = _config["JwtSettings:Issuer"];
            var validAudience = _config["JwtSettings:Audience"];

            var _appUser = await GetUserByEmail(email);
            var userClaims = _appUser.Roles.Split(",").Select(r => new Claim(ClaimTypes.Role, r)).ToList();
            userClaims.Add(new Claim(ClaimTypes.Name, _appUser.Email));
            userClaims.Add(new Claim(ClaimTypes.Email, _appUser.Email));
            userClaims.Add(new Claim(ClaimTypes.NameIdentifier, _appUser.UniqueId.ToString()));
            userClaims.Add(new Claim(ClaimTypes.Sid, await GetEpOrgIdByLoginEmail(email)));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
            var cred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: cred,
                issuer: validIssuer,
                audience: validAudience
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

    }
}
