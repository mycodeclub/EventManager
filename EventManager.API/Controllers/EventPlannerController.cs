using EventManager.API.DataRepository.Implementation;
using EventManager.API.DataRepository.Interfaces;
using EventManager.API.EfData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EventManager.API.DataRepository.Implementation;
using EventManager.Dto.User;
using EventManager.Dto;
using System.Composition;

namespace EventManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventPlannerController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IUserAccountDR _userAccountService;
        private readonly IEventPlannerOrgDR _eventPlannerService;

        public EventPlannerController(AppDbContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _userAccountService = new UserAccountDR(context, _configuration);
            _eventPlannerService = new EventPlannerOrgDR(context);
        }



        [HttpPost("CreateEpOrg")]
        public async Task<ActionResult<EventPlannerOrg>> EpOrgRegister(Dto.EventPlannerOrg epOrg)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _eventPlannerService.CreateEpOrg(epOrg);
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

        [HttpPost("EditEpOrg")]
        public async Task<ActionResult<EventPlannerOrg>> EditMyProfile(Dto.EventPlannerOrg epOrg)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _eventPlannerService.EditMyProfile(epOrg);
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

        [HttpGet("GetEpOrgDetail/{id}")]
        public async Task<ActionResult<EventPlannerOrg>> GetEpEditMyProfile(Guid id)
        {
            if (!id.Equals(Guid.Empty))
            {
                Dto.EventPlannerOrg epOrg = await _eventPlannerService.GetEpOrgDetailsById(id);
                return Ok(epOrg);
            }
            return BadRequest("Invalid Id for a Event Planner role");
        }



        [HttpGet("GetEpOrgByEmail/{email}")]
        public async Task<ActionResult<EventPlannerOrg>> GetEpOrgByEmail(string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                Dto.EventPlannerOrg epOrg = await _eventPlannerService.GetEpOrgByEmail(email);
                return Ok(epOrg);
            }
            return BadRequest("Invalid Profile Id");
        }
    }
}
