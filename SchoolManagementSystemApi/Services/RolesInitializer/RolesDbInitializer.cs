using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemApi.Data;
using SchoolManagementSystemApi.Utilities;

namespace SchoolManagementSystemApi.Services.RolesInitializer
{
    public class RolesDbInitializer : IRolesDbInitializer
    {
        private readonly ApiDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesDbInitializer(
            ApiDbContext context,
            RoleManager<IdentityRole> roleManager)
        {
            _context=context;
            _roleManager=roleManager; 
        }

        public void Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Count()>0)
                {
                    _context.Database.Migrate();
                }
            }
            catch(Exception ex)
            {

            }
            if (_context.Roles.Any(r => r.Name == SD.SuperAdmin)) return;

            ///ROLES CREATION
            _roleManager.CreateAsync(new IdentityRole(SD.SuperAdmin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Student)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Teacher)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Parent)).GetAwaiter().GetResult();
        }
    }
}
