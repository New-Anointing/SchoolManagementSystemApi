using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemApi.Data;
using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Helpers;
using SchoolManagementSystemApi.Model;
using SchoolManagementSystemApi.Services.UserResolver;
using SchoolManagementSystemApi.Utilities;
using System.Net;

namespace SchoolManagementSystemApi.Services.Student
{
    public class StudentsServices : IStudentsServices
    {
        private readonly ApiDbContext _context;
        private readonly IUserResolverServices _userResolverServices;
        private static Students _student = new();
        

        public StudentsServices
        (
            ApiDbContext context,
            IUserResolverServices userResolverServices
        )
        {
            _context = context;
            _userResolverServices = userResolverServices;
        }

        private Guid OrgId => _userResolverServices.GetOrgId();

        public async Task<GenericResponse<IEnumerable<ApplicationUser>>> GetAllStudents()
        {
            try
            {
                var userWithStudentsRole = await _context.ApplicationUser.Where(u => u.OrganisationId == OrgId && u.Role == SD.Student).ToListAsync();
                return new GenericResponse<IEnumerable<ApplicationUser>>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = userWithStudentsRole,
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

        public async Task<GenericResponse<Students>> RegisterStudents(StudentUserDTO result)
        {
            try
            {
                var User = await _context.ApplicationUser.FirstOrDefaultAsync(a => a.OrganisationId == OrgId && a.Id == result.ApplicationUserId.ToString());
                if (User == null)
                {
                    return new GenericResponse<Students>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Data = null,
                        Message = "No user with this id exist",
                        Success = false
                    };
                }
                var isAStudent = await _context.Students.FirstOrDefaultAsync(t => t.OrganisationId == OrgId && t.ApplicationUser.Id == result.ApplicationUserId.ToString() && t.IsDeleted == false);
                if (isAStudent == null && User != null)
                {
                    _student.Id = Guid.NewGuid();
                    _student.OrganisationId = OrgId;
                    _student.ApplicationUser = User;
                    await _context.Students.AddAsync(_student);
                    await _context.SaveChangesAsync();
                    return new GenericResponse<Students>
                    {
                        StatusCode = HttpStatusCode.OK,
                        Data = _student,
                        Message = "User as been made a student",
                        Success = true
                    };
                }
                return new GenericResponse<Students>
                {
                    StatusCode = HttpStatusCode.ExpectationFailed,
                    Data = null,
                    Message = "User has been previously made a student",
                    Success = false
                };
            }
            catch (Exception e)
            {
                return new GenericResponse<Students>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null,
                    Message = $"An error occured: {e.Message}",
                    Success = false
                };
            }
        }

        public async Task<GenericResponse<IEnumerable<Students>>> GetAllRegisteredStudents()
        {
            try
            {
                var regStudents = await _context.Students.Include(s=> s.ApplicationUser).Where(t => t.OrganisationId == OrgId && t.IsDeleted == false).ToListAsync();
                if (regStudents == null)
                {
                    return new GenericResponse<IEnumerable<Students>>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Data = null,
                        Message = "No Students have been registered",
                        Success = false
                    };
                }
                return new GenericResponse<IEnumerable<Students>>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = regStudents,
                    Message = "Data loaded successfully",
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new GenericResponse<IEnumerable<Students>>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null,
                    Message = $"An error occured: {e.Message}",
                    Success = false
                };
            }
        }
    }
}
