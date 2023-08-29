using EventManager.Dto;
using EventManager.Web.Services.Interfaces;
using System.Net.Http;

namespace EventManager.Web.Services.Implementation
{
    public class EventPlannerOrganization : IEventPlannerOrganization
    {
        private readonly HttpClient _httpClient;

        public EventPlannerOrganization(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse> CreateEventPlannerOrg(EventPlannerOrg epOrg)
        { 
            ApiResponse result;
            try
            {
                var response = _httpClient.PostAsJsonAsync("api/EventManager/CreateEpOrg", epOrg).Result;
                result = await response.Content.ReadFromJsonAsync<ApiResponse>();
            }
            catch (Exception ex)
            {
                result = new ApiResponse() { IsCreated = false, ErrorMessages = new List<string>() { "Unable To Create User" } };
                result.ErrorMessages.Add(ex.Message);
            }
            return result;

        }
        public async Task<ApiResponse> EditEventPlannerOrg(EventManager.Dto.EventPlannerOrg epOrg)
        {

            ApiResponse result;
            try
            {
                var response = _httpClient.PostAsJsonAsync("api/EventManager/EditEpOrg", epOrg).Result;
                result = await response.Content.ReadFromJsonAsync<ApiResponse>();
            }
            catch (Exception ex)
            {
                result = new ApiResponse() { IsCreated = false, ErrorMessages = new List<string>() { "Unable To Create User" } };
                result.ErrorMessages.Add(ex.Message);
            }
            return result;

        }         
        public async Task<EventPlannerOrg> GetEpOrgDetail(Guid id)
        {

            EventPlannerOrg epOrg;
            try
            {
                var response = _httpClient.GetFromJsonAsync<EventPlannerOrg>($"api/EventManager/GetEpOrgDetail/{id}");
                epOrg = await response;// .Content.ReadFromJsonAsync<EventPlannerOrg>();
            }
            catch (Exception ex)
            {
                epOrg = new EventPlannerOrg() { UniqueId = id };
            }
            return epOrg;

        }

        }
}
