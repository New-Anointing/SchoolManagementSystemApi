using Microsoft.AspNetCore.Identity;
using SchoolManagementSystemApi.Data;
using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Model;
using SchoolManagementSystemApi.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SchoolManagementSystemApi.Services.SchoolRegistration
{
    public class RegServices : IRegServices
    {
        private readonly ApiDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private IConfiguration _configuration;
        private static ApplicationUser user = new();
        public RegServices
        (
            ApiDbContext context,
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
        )
        {
            _context = context;
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
        }





        public async Task<ApplicationUser> SchoolRegistration(AdminUserDTO request)
        {
            var userExist = await _userManager.FindByEmailAsync(request.EmailAddress);
            if (userExist != null)
            {
                throw new InvalidOperationException("user with this email already exist");
            }
            //ORGANISATION
            var Org = new Organisation()
            {
                Id = Guid.NewGuid().ToString(),
                SchoolName = request.SchoolName,
                Address = request.SchoolAddress
            };

            _context.Organisation.Add(Org);


            user.Email = request.EmailAddress;
            user.UserName = request.EmailAddress;
            user.HomeAdddress = request.HomeAdddress;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;
            user.Role = "SuperAdmin";
            user.OrgId = Org.Id;
            user.Gender = request.Gender;
            user.DateOfBirth = request.DateOfBirth;

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Something went wrong :(");
            }
            else
            {
                //ROLES CREATION
                if (!await _roleManager.RoleExistsAsync(SD.SuperAdmin))
                {
                    await _roleManager.CreateAsync(new IdentityRole(SD.SuperAdmin));
                }
                if (!await _roleManager.RoleExistsAsync(SD.Admin))
                {
                    await _roleManager.CreateAsync(new IdentityRole(SD.Admin));
                }
                if (!await _roleManager.RoleExistsAsync(SD.Teacher))
                {
                    await _roleManager.CreateAsync(new IdentityRole(SD.Teacher));
                }
                if (!await _roleManager.RoleExistsAsync(SD.Student))
                {
                    await _roleManager.CreateAsync(new IdentityRole(SD.Student));
                }
                if (!await _roleManager.RoleExistsAsync(SD.Parent))
                {
                    await _roleManager.CreateAsync(new IdentityRole(SD.Parent));
                }

                //ASSIGNING SUPER ADMIN ROLE
                await _userManager.AddToRoleAsync(user, SD.SuperAdmin);

            }
            _context.SaveChanges();
            return user;

        }

    }
}
