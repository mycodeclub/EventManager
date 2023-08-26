using Microsoft.EntityFrameworkCore;

namespace EventManager.API.EfData
{
    public static class ModelBuilderExtention
    {
        public static void SeedRoles(this ModelBuilder modelBuilder)
        {
            StaticData.ApplicationRoles.ForEach(role => { modelBuilder.Entity<Dto.User.AppRole>().HasData(role); });
        }
    }
}
