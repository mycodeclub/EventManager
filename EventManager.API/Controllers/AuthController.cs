using EventManager.Dto.User;
using EventManager.Dto;
using Microsoft.AspNetCore.Mvc;
using EventManager.API.EfData;
using EventManager.API.DataRepository.Implementation;
using EventManager.API.DataRepository.Interfaces;
using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EventManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly IUserAccountDR userAccountService;
        private readonly IEventPlannerOrgDR eventPlannerService;
        private readonly AppDbContext context;
        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            userAccountService = new UserAccountDR(context, _configuration);
            eventPlannerService = new EventPlannerOrgDR(context);
            this.context = context;
        }

        [HttpPost("EventOrgRegistration")]
        public async Task<ActionResult<EventPlannerOrg>> EpOrgRegister(UserRegistrationVM _user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await eventPlannerService.RegisterEventPlanner(_user, new UserAccountDR(context, _configuration));
                    return result.IsCreated ? Ok(result) : BadRequest(result);
                }
                catch (Exception ex) { return BadRequest(ex.Message); }
            }
            return BadRequest("Invalid Input");
        }

        [HttpPost("Login")]
        public async Task<ActionResult<EventPlannerOrg>> Login(LoginVM loginRequest)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await userAccountService.Login(loginRequest.LoginName, loginRequest.Password);
                    if (result.IsLoginSuccess)
                        return Ok(result);
                    else if (result.ErrorMessages != null && result.ErrorMessages.Count > 0)
                        return BadRequest(string.Join(",", result.ErrorMessages));
                    else
                        return BadRequest("Something went wrong, please try again");
                }
                catch (Exception ex) { return BadRequest(ex.Message); }
            }
            else return BadRequest("Invalid Data");
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<EventPlannerOrg>> GetAllUsers()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await userAccountService.GetAllUsers();
                    return Ok(result);

                }
                catch (Exception ex) { return BadRequest(ex.Message); }
            }
            else return BadRequest("Invalid Data");
        }

        //--------------------------------------------------------------------------------------
        //        [HttpPost("UserRegistration")]
        private async Task<ActionResult<EventPlannerOrg>> Register(UserRegistrationVM _user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await userAccountService.RegisterNewUser(_user, null);
                    if (result.IsCreated)
                        return Ok(result);
                    else if (result.ErrorMessages != null && result.ErrorMessages.Count > 0)
                        return BadRequest(string.Join(",", result.ErrorMessages));
                    else
                        return BadRequest("Something went wrong, please try again");
                }
                catch (Exception ex) { return BadRequest(ex.Message); }
            }
            return BadRequest("Invalid Input");
        }

    }
}
