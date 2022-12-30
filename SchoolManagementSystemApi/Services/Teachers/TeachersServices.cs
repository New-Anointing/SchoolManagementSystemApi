using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemApi.Data;
using SchoolManagementSystemApi.Helpers;
using SchoolManagementSystemApi.Model;
using SchoolManagementSystemApi.Services.UserResolver;
using SchoolManagementSystemApi.Utilities;
using System.Net;

namespace SchoolManagementSystemApi.Services.Teachers
{
    public class TeachersServices : ITeachersServices
    {
        private readonly ApiDbContext _context;
        private readonly IUserResolverServices _userResolverServices;

        public TeachersServices
        (
            ApiDbContext context,
            IUserResolverServices userResolverServices
        )
        {
            _context=context;
            _userResolverServices=userResolverServices;
        }
        private Guid OrgId => _userResolverServices.GetOrgId();
        public async Task<GenericResponse<IEnumerable<ApplicationUser>>> GetAllTeachers()
        {
            try
            {
                var userWithTeachersRole = await _context.ApplicationUser.Where(u => u.OrganisationId == OrgId && u.Role == SD.Teacher).ToListAsync();
                return new GenericResponse<IEnumerable<ApplicationUser>>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = userWithTeachersRole,
                    Message = "Data loaded successfully",
                    Success = true
                };

            }
            catch (Exception e)
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
    }
}
