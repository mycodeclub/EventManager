using EventManager.Dto;
using EventManager.Dto.User;
using EventManager.Web.Services.Interfaces;
using System.Net.Http.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EventManager.Web.Services.Implementation
{
    public class Account : IAccount
    {
        private readonly HttpClient _httpClient;

        public Account(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginResponse> Login(LoginVM loginRequest)
        {

            LoginResponse result;
            try
            {
                var response = _httpClient.PostAsJsonAsync("api/Auth/Login", loginRequest).Result;
                result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            }
            catch (Exception ex)
            {
                result = new() { ErrorMessages = new List<string>() { "Unable To Login" }, Token = string.Empty, IsLoginSuccess = false };
                result.ErrorMessages.Add(ex.Message);
            }
            return result;

        }

        public async Task<ApiResponse> SignUp(UserRegistrationVM userRegistrationRequest)
        {
            ApiResponse result;
            try
            {
                var response = _httpClient.PostAsJsonAsync("api/Auth/Registration", userRegistrationRequest).Result;
                result = await response.Content.ReadFromJsonAsync<ApiResponse>();
            }
            catch (Exception ex)
            {
                result = new ApiResponse() { IsCreated = false, ErrorMessages = new List<string>() { "Unable To Create User" } };
                result.ErrorMessages.Add(ex.Message);
            }
            return result;
        }
        public async Task<ApiResponse> EventOrgSignUp(UserRegistrationVM userRegistrationRequest)
        {
            ApiResponse result;
            try
            {
                var response = _httpClient.PostAsJsonAsync("api/Auth/EventOrgRegistration", userRegistrationRequest).Result;
                result = await response.Content.ReadFromJsonAsync<ApiResponse>();
            }
            catch (Exception ex)
            {
                result = new ApiResponse() { IsCreated = false, ErrorMessages = new List<string>() { "Unable To Create User" } };
                result.ErrorMessages.Add(ex.Message);
            }
            return result;
        }
        public async Task<IEnumerable<EventManager.Dto.User.AppUser>> GetAllUsers()
        {
            var users = await _httpClient.GetFromJsonAsync<List<AppUser>>("api/Auth/GetAllUsers");
            return users;

        }
    }
}
