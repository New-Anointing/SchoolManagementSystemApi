using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Helpers;
using SchoolManagementSystemApi.Model;

namespace SchoolManagementSystemApi.Services.Student
{
    public interface IStudentsServices
    {
        Task<GenericResponse<IEnumerable<ApplicationUser>>> GetAllStudents();
        Task<GenericResponse<Students>> RegisterStudents(StudentUserDTO result);
        Task<GenericResponse<IEnumerable<Students>>> GetAllRegisteredStudents();
    }
}
