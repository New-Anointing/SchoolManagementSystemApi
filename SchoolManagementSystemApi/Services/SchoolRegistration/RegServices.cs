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
        private readonly UserManager<OrganisationRegistration> _userManager;
        private IConfiguration _configuration;
        public static OrganisationRegistration user = new();
        public RegServices
        (
            ApiDbContext context,
            IConfiguration configuration,
            UserManager<OrganisationRegistration> userManager,
            RoleManager<IdentityRole> roleManager
        )
        {
            _context = context;
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
        }





        public async Task<OrganisationRegistration> SchoolRegistration(SchoolRegistrationDTO request)
        {
            var userExist = await _userManager.FindByEmailAsync(request.Email);
            if (userExist != null)
            {
                throw new InvalidOperationException("user with this email already exist");
            }
            //ORGANISATION
            var Org = new Organisation()
            {
                Id = Guid.NewGuid().ToString(),
                OrganisationName = request.SchoolName,
            };

            _context.Organisation.Add(Org);

        

            CreatePasswordSalt(request.Password, out byte[] passwordSalt);
            user.Email = request.Email;
            user.SchoolName = request.SchoolName;
            user.UserName = request.SchoolName;
            user.PasswordSalt = passwordSalt;
            user.Address = request.Address;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;
            user.Role = "SuperAdmin";
            user.OrgId = Org.Id;

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

        private void CreatePasswordSalt(string password, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
            }
        }

       


    }
}
