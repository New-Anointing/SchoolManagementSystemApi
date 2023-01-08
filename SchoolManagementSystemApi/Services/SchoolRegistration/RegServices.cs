using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemApi.Data;
using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Helpers;
using SchoolManagementSystemApi.Model;
using SchoolManagementSystemApi.Services.UserResolver;
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
        private readonly IUserResolverServices _userResolverService;
        private static ApplicationUser user = new();
        public RegServices
        (
            ApiDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IHttpContextAccessor httpContextAccessor,
            IUserResolverServices userResolverServices
        )
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
            _userResolverService = userResolverServices;
        }

        private Guid OrgId => _userResolverService.GetOrgId();



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
                var orgId = Guid.NewGuid();
                var Org = new Organisation()
                {
                    Id = orgId.ToString(),
                    SchoolName = request.SchoolName,
                    Address = request.SchoolAddress,
                    OrganisationId = orgId
                };

                _context.Organisation.Add(Org);
                

                user.Email = request.EmailAddress;
                user.UserName = request.EmailAddress;
                user.HomeAddress = request.HomeAddress;
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.PhoneNumber = request.PhoneNumber;
                user.Role = SD.SuperAdmin;
                user.OrganisationId = Guid.Parse(Org.Id);
                user.Gender = request.Gender.ToString();
                user.DateOfBirth = request.DateOfBirth;

                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                {
                    return new GenericResponse<ApplicationUser>
                    {
                        StatusCode = HttpStatusCode.ExpectationFailed,
                        Data = null,
                        Message = "Something went wrong :(" +
                        "Check and validate all inputs",
                        Success = false
                    };
                }
                else
                {
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
                user.HomeAddress = request.HomeAddress;
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.PhoneNumber = request.PhoneNumber;
                user.OrganisationId = OrgId;
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
                        Message = "Something went wrong :( " +
                        "Check and validate all inputs",
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

        public async Task<GenericResponse<IEnumerable<ApplicationUser>>> GetAllUsers()
        {
            try
            {
                var applicationUsers = await _context.ApplicationUser.Where(a => a.OrganisationId == OrgId).ToListAsync();
                return new GenericResponse<IEnumerable<ApplicationUser>>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = applicationUsers,
                    Message = "Data loaded successfully",
                    Success = true
                };
                
            }
            catch(Exception e)
            {
                return new GenericResponse<IEnumerable<ApplicationUser>>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null,
                    Message = "An error occurred: " + e.Message,
                    Success = false
                };
            }
        }

        public async Task<GenericResponse<ApplicationUser>> GetUserById(string id)
        {
            try
            {
                var applicationUser =  await _context.ApplicationUser.FirstOrDefaultAsync(c => c.Id == id);
                if (applicationUser == null)
                {
                    return new GenericResponse<ApplicationUser>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Data = null,
                        Message = "No User with this id exist :(",
                        Success = false
                    };
                }
                return new GenericResponse<ApplicationUser>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = applicationUser,
                    Message = "Data loaded successfully!",
                    Success = true
                };
            }
            catch(Exception e)
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
