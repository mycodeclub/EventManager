using EventManager.Dto.User;
using EventManager.Dto;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Blazored.LocalStorage;


using Blazored.LocalStorage;
using EventManager.Dto.User;
using Microsoft.AspNetCore.Components;

namespace EventManager.Pages.Account
{
    public partial class Login
    {
        [Inject]
        protected Web.Services.Interfaces.IAccount accountService { get; set; }



        public static LoginVM logInRequest { get; set; } = new();

        public static LoginResponse _response { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Inject]
        protected ILocalStorageService _localStorage { get; set; }

        public async void SubmitLoginRequest()
        {
            var result = accountService.Login(logInRequest);
            _response = result.Result;
            if (_response.IsLoginSuccess)
            {
                var tokenStr = _response.Token;
                _localStorage.SetItemAsync("jwt_token", tokenStr);
                _localStorage.SetItemAsync("EpOrgId", _response.EpOrgId);
                NavigationManager.NavigateTo("/EventOrganizer/Dashboard");
            }
            else
                Console.WriteLine("Some thing went wrong, " + string.Join(", ", _response.ErrorMessages));
            var token = _response.Token;
        }

    }
}
