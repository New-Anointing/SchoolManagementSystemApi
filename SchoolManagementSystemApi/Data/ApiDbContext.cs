using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemApi.Model;

namespace SchoolManagementSystemApi.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {

        }
        public DbSet<OrganisationRegistration> OrganisationReg { get; set; }
    }
}
