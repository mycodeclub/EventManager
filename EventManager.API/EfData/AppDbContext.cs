using EventManager.Dto.User;
using EventManager.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Reflection.Metadata;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManager.API.EfData
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //            modelBuilder.SeedRoles();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) { }
        }

        public DbSet<AppUser> AppUsers { get; set; }
        //      public DbSet<AppRole> AppRoles { get; set; }
        //      public DbSet<UserRole> UserRoles { get; set; }



        //[NotMapped]
        //public async Task<int> CallSp(int par1, int par2)
        //{
        //    var parameter = new SqlParameter("@Parameter1", par1);
        //    return await Database.ExecuteSqlRawAsync("EXEC MyStoredProcedure @ParameterName", parameter);
        //}


        public DbSet<EventPlannerOrg> EventPlanners { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<GuestEventAttendance> EventAttendances { get; set; }
    }
}
