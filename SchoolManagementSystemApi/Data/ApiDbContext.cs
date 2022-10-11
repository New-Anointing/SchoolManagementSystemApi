using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemApi.Model;

namespace SchoolManagementSystemApi.Data
{
    public class ApiDbContext : IdentityDbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {

        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Organisation> Organisation { get; set; }
        public DbSet<ClassRoom> ClassRoom { get; set; }
        public DbSet<Subjects> Subjects { get; set; }
        public DbSet<TimeTable> TimeTable { get; set; }
        public DbSet<Teachers> Teachers { get; set; }
        public DbSet<Events> Events { get; set; }


    }
}
