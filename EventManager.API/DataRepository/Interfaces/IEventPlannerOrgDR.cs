using EventManager.API.DataRepository.Implementation;
using EventManager.Dto;
using EventManager.Dto.User;

namespace EventManager.API.DataRepository.Interfaces
{
    public interface IEventPlannerOrgDR
    {
        public Task<ApiResponse> CreateEpOrg(EventPlannerOrg epOrg);
        public Task<ApiResponse> RegisterEventPlanner(UserRegistrationVM request, UserAccountDR userAccount);
        public Task<ApiResponse> EditMyProfile(EventPlannerOrg epOrg);
        public Task<EventPlannerOrg> GetEpOrgDetailsById(Guid id);
        Task<EventPlannerOrg> GetEpOrgByEmail(string email);
    }
}
