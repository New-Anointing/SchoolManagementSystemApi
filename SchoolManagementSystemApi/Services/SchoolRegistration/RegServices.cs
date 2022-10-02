using Microsoft.AspNetCore.Identity;
using SchoolManagementSystemApi.Data;
using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Helpers;
using SchoolManagementSystemApi.Model;
using SchoolManagementSystemApi.Utilities;
using System.Net;
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


        private string GetOrg()
        {
            string claim = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                claim = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            var orgId = _context.ApplicationUser.FirstOrDefault(c => c.Id == claim)?.OrgId;

            return orgId;
        }



        public async Task<GenericResponse<ApplicationUser>> SchoolRegistration(AdminUserDTO request)
        {
            try
            {
                var userExist = await _userManager.FindByEmailAsync(request.EmailAddress);
                if (userExist != null)
                {
                    return new GenericResponse<ApplicationUser>
                    {
                        StatusCode = HttpStatusCode.ExpectationFailed,
                        Data = null,
                        Message = "User with this email already exist",
                        Success = false
                    };
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
                    return new GenericResponse<ApplicationUser>
                    {
                        StatusCode = HttpStatusCode.ExpectationFailed,
                        Data = null,
                        Message = "Something went wrong :(",
                        Success = false
                    };
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
                return new GenericResponse<ApplicationUser>
                {
                    StatusCode = HttpStatusCode.Created,
                    Data = user,
                    Message = "Registration is successful",
                    Success = true
                };
            }
            catch (Exception e)
            {

                return new GenericResponse<ApplicationUser>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null,
                    Message = "An error occurred: " + e.Message,
                    Success = false
                };
            }


        }

        public async Task<GenericResponse<ApplicationUser>> UserRegistration(UserDTO request)
        {
            try
            {
                var userExist = await _userManager.FindByEmailAsync(request.EmailAddress);
                if (userExist != null)
                {
                    return new GenericResponse<ApplicationUser>
                    {
                        StatusCode = HttpStatusCode.ExpectationFailed,
                        Data = null,
                        Message = "User with this email already exist",
                        Success = false
                    };
                }

                user.UserName = request.EmailAddress;
                user.Email = request.EmailAddress;
                user.HomeAdddress = request.HomeAdddress;
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.PhoneNumber = request.PhoneNumber;
                user.OrgId = GetOrg();
                user.Gender = request.Gender.ToString();
                user.DateOfBirth = request.DateOfBirth;
                user.Role = request.Role.ToString();

                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                {
                    return new GenericResponse<ApplicationUser>
                    {
                        StatusCode = HttpStatusCode.ExpectationFailed,
                        Data = null,
                        Message = "Something went wrong :(",
                        Success = false
                    };
                }
                else
                {
                    //ASSIGNING ROLES TO CREATED USER
                    if (request.Role.ToString() == SD.Admin)
                    {
                        await _userManager.AddToRoleAsync(user, SD.Admin);
                    }
                    else if (request.Role.ToString() == SD.Teacher)
                    {
                        await _userManager.AddToRoleAsync(user, SD.Teacher);
                    }
                    else if (request.Role.ToString() == SD.Student)
                    {
                        await _userManager.AddToRoleAsync(user, SD.Student);
                    }
                    else if (request.Role.ToString() == SD.Parent)
                    {
                        await _userManager.AddToRoleAsync(user, SD.Teacher);
                    }
                }
                return new GenericResponse<ApplicationUser>
                {
                    StatusCode = HttpStatusCode.Created,
                    Data = user,
                    Message = "User is created successfully",
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new GenericResponse<ApplicationUser>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null,
                    Message = "An error occurred: " + e.Message,
                    Success = false
                };
            }

        }
    }
}
