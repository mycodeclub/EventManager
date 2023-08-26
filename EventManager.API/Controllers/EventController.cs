using EventManager.API.DataRepository.Implementation;
using EventManager.API.DataRepository.Interfaces;
using EventManager.API.EfData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EventManager.API.DataRepository.Implementation;
using EventManager.Dto.User;
using EventManager.Dto;
using System.Composition;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using EventManager.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace EventManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "EventPlannerOrg")]
    [Authorize(Policy = "EventPlannerOrg")]

    public class EventController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IUserAccountDR _userAccountService;
        private readonly IEventPlannerOrgDR _eventPlannerService;
        private readonly LoggedInUserData _loggedInUser;
        private readonly AppDbContext _dbContext;
        public EventController(AppDbContext context, IConfiguration configuration)
        {
            _dbContext = context;
            _configuration = configuration;
            _userAccountService = new UserAccountDR(context, _configuration);
            _eventPlannerService = new EventPlannerOrgDR(context);
            _loggedInUser = new LoggedInUserData(HttpContext);
        }


        [HttpPost("SaveEvent")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents(Dto.Event eventDto)
        {
            if (!Guid.Empty.Equals(_loggedInUser.LoggedInUserId))
            {
                if (Guid.Empty.Equals(eventDto.UniqueId))
                    _dbContext.Events.Add(eventDto);
                else _dbContext.Events.Update(eventDto);
                await _dbContext.SaveChangesAsync();
            }

            var myEvents = await _dbContext.Events.Where(e => e.EventOrganizerId.Equals(_loggedInUser.LoggedInUserId)).ToListAsync();
            return Ok(myEvents);
        }

        [HttpGet("GetEvents")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            var myEvents = await _dbContext.Events.Where(e => e.EventOrganizerId.Equals(_loggedInUser.LoggedInUserId)).ToListAsync();
            return Ok(myEvents);
        }
        [HttpGet("GetEvent/{id}")]
        public async Task<ActionResult<Event>> GetEvent(Guid? id)
        {
            if (!id.HasValue) id = new Guid();
            if (!id.Equals(Guid.Empty))
            {
                var myEvents = await _dbContext.Events.Where(e => e.UniqueId.Equals(id.Value) && e.EventOrganizerId.Equals(_loggedInUser.LoggedInUserId)).FirstOrDefaultAsync();
                return Ok(myEvents);
            }
            return BadRequest("Invalid Profile Id");
        }

    }
}
