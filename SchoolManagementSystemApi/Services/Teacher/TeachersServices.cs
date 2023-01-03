using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemApi.Data;
using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Helpers;
using SchoolManagementSystemApi.Model;
using SchoolManagementSystemApi.Services.UserResolver;
using SchoolManagementSystemApi.Utilities;
using System.Net;

namespace SchoolManagementSystemApi.Services.Teacher
{
    public class TeachersServices : ITeachersServices
    {
        private readonly ApiDbContext _context;
        private readonly IUserResolverServices _userResolverServices;
        private static Teachers _teacher = new();

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
        public async Task<GenericResponse<Teachers>> RegisterTeachers(TeacherUserDTO result)
        {
            try
            {
                var User = await _context.ApplicationUser.FirstOrDefaultAsync(a => a.OrganisationId == OrgId && a.Id == result.ApplicationUserId.ToString());
                if (User == null)
                {
                    return new GenericResponse<Teachers>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Data = null,
                        Message = "No user with this id exist",
                        Success = false
                    };
                }
                var isATeacher = await _context.Teachers.FirstOrDefaultAsync(t => t.OrganisationId == OrgId && t.ApplicationUser.Id == result.ApplicationUserId.ToString() && t.IsDeleted == false);
                if (isATeacher == null && User != null)
                {
                    _teacher.Id = Guid.NewGuid();
                    _teacher.OrganisationId = OrgId;
                    _teacher.ApplicationUser = User;
                    await _context.Teachers.AddAsync(_teacher);
                    await _context.SaveChangesAsync();
                    return new GenericResponse<Teachers>
                    {
                        StatusCode = HttpStatusCode.OK,
                        Data = _teacher,
                        Message = "User as been made a teacher",
                        Success = true
                    };
                }
                return new GenericResponse<Teachers>
                {
                    StatusCode = HttpStatusCode.ExpectationFailed,
                    Data = null,
                    Message = "User has been previously made a teacher",
                    Success = false
                };
            }
            catch (Exception e)
            {
                return new GenericResponse<Teachers>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null,
                    Message = $"An error occured: {e.Message}",
                    Success = false
                };
            }
        }

        public async Task<GenericResponse<IEnumerable<Teachers>>> GetAllRegisteredTeachers()
        {
            try
            {
                var regTeachers = await _context.Teachers.Include(t => t.ApplicationUser).Where(t => t.OrganisationId == OrgId && t.IsDeleted == false).ToListAsync();
                if(regTeachers == null)
                {
                    return new GenericResponse<IEnumerable<Teachers>>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Data = null,
                        Message = "No Teachers have been registered",
                        Success = false
                    };
                }
                return new GenericResponse<IEnumerable<Teachers>>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = regTeachers,
                    Message = "Data loaded successfully",
                    Success = true
                };
            }
            catch(Exception e)
            {
                return new GenericResponse<IEnumerable<Teachers>>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null,
                    Message = $"An error occured: {e.Message}",
                    Success = false
                };
            }
        }

        public async Task<GenericResponse<Teachers>> AssignClassTeachers(ClassTeacherDTO result, Guid TeacherId)
        {
            try
            {
                var classRoomExist = await _context.ClassRoom.FirstOrDefaultAsync(s => s.Id == result.ClassRoomId && s.OrganisationId == OrgId && s.IsDeleted == false);
                if(classRoomExist == null)
                {
                    return new GenericResponse<Teachers>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Data = null,
                        Message = "Invalid Classroom Id Classroom Does Not Exist",
                        Success = false
                    };
                }
                var teacher = await _context.Teachers.Include(u=>u.ApplicationUser).FirstOrDefaultAsync(t=>t.Id == TeacherId && t.OrganisationId == OrgId && t.IsDeleted == false);
                if(teacher != null)
                {

                    teacher.ClassRoomID = result.ClassRoomId;
                    _context.Update(teacher);
                    await _context.SaveChangesAsync();
                    return new GenericResponse<Teachers>
                    {
                        StatusCode = HttpStatusCode.OK,
                        Data = teacher,
                        Message = "Class Teacher assigned successfully",
                        Success = true
                    };
                }
                return new GenericResponse<Teachers>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Data = null,
                    Message = "No Teacher with this id exist",
                    Success = false
                };

            }
            catch(Exception e)
            {
                return new GenericResponse<Teachers>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null,
                    Message = $"An error occured: {e.Message}",
                    Success = false
                };
            }
        }

        public Task<GenericResponse<Teachers>> AssignSubjectTeachers(SubjectTeacherDTO result, Guid TeacherId)
        {
            throw new NotImplementedException();
        }
    }
}
