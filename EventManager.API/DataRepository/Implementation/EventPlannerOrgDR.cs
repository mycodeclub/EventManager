using Azure.Core;
using EventManager.API.DataRepository.Interfaces;
using EventManager.API.EfData;
using EventManager.Dto;
using EventManager.Dto.User;
using Microsoft.EntityFrameworkCore;
using System.Composition;

namespace EventManager.API.DataRepository.Implementation
{
    public class EventPlannerOrgDR : IEventPlannerOrgDR
    {
        //        private UserAccount _userAccount;
        private AppDbContext _context;
        public EventPlannerOrgDR(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse> CreateEpOrg(EventPlannerOrg epOrg)
        {
            var response = new ApiResponse();
            try
            {
                await _context.EventPlanners.AddAsync(epOrg);
                await _context.SaveChangesAsync();
                if (epOrg.UniqueId != Guid.Empty)
                {
                    response.IsCreated = true;
                    response.SuccessMessages = new List<string>() { "Event Organizor Created Successfully" };
                }
            }
            catch
            {
                response.ErrorMessages = new List<string>() { "Unable to create Organizor Profile, please contact to support with reference UserId " + epOrg.AppUserId };
            };
            return response;
        }

        public async Task<ApiResponse> RegisterEventPlanner(UserRegistrationVM request, UserAccountDR userAccount)
        {
            var result = await userAccount.RegisterNewUser(request, StaticData.ApplicationUserRolesEnum.EventPlannerOrg.ToString());
            if (result.IsCreated)
            {
                await _context.EventPlanners.AddAsync(new EventPlannerOrg()
                {
                    AppUserId = result.UniqueId,
                    OrgName = request.OrganizationName,
                    CreatedDate = DateTime.UtcNow
                });
                var saveEpoResult = await _context.SaveChangesAsync();
                result.IsCreated = saveEpoResult > 0;
            }
            return result;
        }


        public async Task<ApiResponse> EditMyProfile(EventPlannerOrg epOrg)
        {
            var response = new ApiResponse();
            try
            {
                epOrg.LastModifiedDate = DateTime.UtcNow;
                _context.EventPlanners.Update(epOrg);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    response.SuccessMessages = new List<string>() { "Event Organizor Updated Successfully" };
                    response.IsCreated = true;
                    return response;
                }
            }
            catch
            {
                response.ErrorMessages ??= new List<string>() { };
                response.ErrorMessages.Add("Unable to Update Organizor Profile, please contact to support with reference UserId " + epOrg.AppUserId);
            };
            return response;
        }

        public async Task<EventPlannerOrg> GetEpOrgDetailsById(Guid id)
        {
            var dbOrg = await _context.EventPlanners
                .Include(c => c.PointOfContact)
                .Where(org => org.UniqueId.Equals(id))
                .FirstOrDefaultAsync();

            return dbOrg;
        }

        public async Task<EventPlannerOrg> GetEpOrgByEmail(string email)
        {
            var _query = from epo in _context.EventPlanners
                         join user in _context.AppUsers on epo.AppUserId equals user.UniqueId
                         where user.Email == email
                         select new { epo };
            var _result = await _query.FirstOrDefaultAsync();
            return _result.epo;
        }
    }
}
