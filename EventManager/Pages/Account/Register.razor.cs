using EventManager.Dto;
using EventManager.Dto.User;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EventManager.Pages.Account
{
    public partial class Register : ComponentBase
    {
        [Inject]
        protected Web.Services.Interfaces.IAccount accountService { get; set; }
        [Inject]
        protected Web.Services.Interfaces.IEventPlannerOrganization epOrgService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }


        public static UserRegistrationVM UserRegistrationRequest { get; set; } = new UserRegistrationVM();

        public static ApiResponse _response { get; set; }

        public static IEnumerable<AppUser> _appUsers { get; set; }



        public async void SubmitUserRegistration()
        {

            _response = accountService.EventOrgSignUp(UserRegistrationRequest).Result;
            if (_response.IsCreated)
                 NavigationManager.NavigateTo("/EventOrganizer/Dashboard");
             else
                UserRegistrationRequest.ErrorMessage = string.Join(", ", _response?.ErrorMessages);
        }


        // ------------------------------------------------------------------

        // Events that run every time when component re-render.
        protected override void OnParametersSet() { base.OnParametersSet(); }
        protected override async Task OnParametersSetAsync() { }
        // LifeCycle
        protected override void OnAfterRender(bool firstRender) { base.OnAfterRender(firstRender); }
        protected override bool ShouldRender() { return base.ShouldRender(); }



        // Events that only run whtn the componetnts are created the first time.

        public override Task SetParametersAsync(ParameterView parameters) { return base.SetParametersAsync(parameters); }
        protected override void OnInitialized() { base.OnInitialized(); }
        protected override async Task OnInitializedAsync() { base.OnInitialized(); }

    }
}
