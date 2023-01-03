using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Helpers;
using SchoolManagementSystemApi.Model;

namespace SchoolManagementSystemApi.Services.Teacher
{
    public interface ITeachersServices
    {
        Task<GenericResponse<IEnumerable<ApplicationUser>>> GetAllTeachers();
        Task<GenericResponse<Teachers>> RegisterTeachers(TeacherUserDTO result);
        Task<GenericResponse<IEnumerable<Teachers>>> GetAllRegisteredTeachers();
        Task<GenericResponse<Teachers>> AssignClassTeachers(ClassTeacherDTO result, Guid TeacherId);
        Task<GenericResponse<Teachers>> AssignSubjectTeachers(SubjectTeacherDTO result, Guid TeacherId);
    }
}
