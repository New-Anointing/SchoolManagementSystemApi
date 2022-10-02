using Microsoft.AspNetCore.Identity;
using SchoolManagementSystemApi.Data;
using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Model;
using SchoolManagementSystemApi.Utilities;
using System.Security.Claims;

namespace SchoolManagementSystemApi.Services.SchoolRegistration
{
    public class RegServices : IRegServices
    {
        private readonly ApiDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static ApplicationUser user = new();
        public RegServices
        (
            ApiDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }


        private Guid GetOrg()
        {
            string claim = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                claim = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            var orgId = _context.ApplicationUser.Where(c => c.Id == claim).FirstOrDefault().OrganisationId;

            return orgId;
        }



        public async Task<ApplicationUser> SchoolRegistration(AdminUserDTO request)
        {
            var userExist = await _userManager.FindByEmailAsync(request.EmailAddress);
            if (userExist != null)
            {
                throw new InvalidOperationException("user with this email already exist");
            }
            //ORGANISATION
            var OrgId = Guid.NewGuid();

            var Org = new Organisation()
            {
                Id = OrgId.ToString(),
                OrganisationId = OrgId,
                SchoolName = request.SchoolName,
                Address = request.SchoolAddress,
            };

            _context.Organisation.Add(Org);


            user.Email = request.EmailAddress;
            user.UserName = request.EmailAddress;
            user.HomeAddress = request.HomeAddress;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;
            user.Role = "SuperAdmin";
            user.OrganisationId = Org.OrganisationId;
            user.Gender = request.Gender;
            user.DateOfBirth = request.DateOfBirth;

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Something went wrong :(");
            }
            else
            {
                //ASSIGNING SUPER ADMIN ROLE
                await _userManager.AddToRoleAsync(user, SD.SuperAdmin);

            }
            _context.SaveChanges();
            return user;

        }

        public async Task<ApplicationUser> UserRegistration(UserDTO request)
        {
            var userExist = await _userManager.FindByEmailAsync(request.EmailAddress);
            if (userExist != null)
            {
                throw new InvalidOperationException("user with this email already exist");
            }

            user.UserName = request.EmailAddress;
            user.Email = request.EmailAddress;
            user.HomeAddress = request.HomeAddress;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;
            user.OrganisationId = GetOrg();
            user.Gender = request.Gender.ToString();
            user.DateOfBirth = request.DateOfBirth;
            user.Role = request.Role.ToString();

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Something went wrong :(");
            }
            else
            {
                //ASSIGNING ROLES TO CREATED USER
                if(request.Role.ToString() == SD.Admin)
                {
                    await _userManager.AddToRoleAsync(user, SD.Admin);
                }else if(request.Role.ToString() == SD.Teacher)
                {
                    await _userManager.AddToRoleAsync(user, SD.Teacher);
                }else if(request.Role.ToString() ==SD.Student)
                {
                    await _userManager.AddToRoleAsync(user, SD.Student);
                }else if(request.Role.ToString() == SD.Parent)
                {
                    await _userManager.AddToRoleAsync(user, SD.Teacher);
                }
            }
            return user;
        }
    }
}
