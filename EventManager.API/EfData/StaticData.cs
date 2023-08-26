using EventManager.Dto.User;

namespace EventManager.API.EfData
{
    public static class StaticData
    {
        public static List<AppRole> ApplicationRoles => new() {
                    new AppRole() { RoleId = new Guid("da576a70-2276-4872-a496-6765a07534e6"), RoleName = "SuperAdmin" },
                    new AppRole() { RoleId = new Guid("44730987-28b6-4c76-8411-30d9429cb2a1"), RoleName = "EventPlannerOrg" },
                    new AppRole() { RoleId = new Guid("f8f904ad-8f56-453b-b96f-d0478badcea2"), RoleName = "EventManagerForOrg" },
                    new AppRole() { RoleId = new Guid("699f2f82-1ff4-11ee-be56-0242ac120002"), RoleName = "TicketScanner" },
                    new AppRole() { RoleId = new Guid("710cbf78-1ff4-11ee-be56-0242ac120002"), RoleName = "Guest" }
                };


        public enum ApplicationUserRolesEnum
        {
            SuperAdmin,
            EventPlannerOrg,
            EventManagerForOrg,
            TicketScanner, Guest
        }
    }
}
