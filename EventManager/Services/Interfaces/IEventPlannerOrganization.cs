using EventManager.Dto;

namespace EventManager.Web.Services.Interfaces
{
    public interface IEventPlannerOrganization
    {
        public Task<ApiResponse> CreateEventPlannerOrg(EventPlannerOrg epOrg);
        public Task<ApiResponse> EditEventPlannerOrg(EventPlannerOrg epOrg);
        public Task<EventPlannerOrg> GetEpOrgDetail(Guid id);
    }
}
