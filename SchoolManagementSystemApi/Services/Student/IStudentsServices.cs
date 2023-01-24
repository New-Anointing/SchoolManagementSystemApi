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
        Task<GenericResponse<Students>> GetRegisteredStudentsById(Guid StudentId);
        Task<GenericResponse<Students>> AssignClassToStudents(Guid StudentId, StudentsClassDTO result);
        Task<GenericResponse<Students>> AssignSubjectsToStudents(Guid StudentId, StudentsSubjectsDTO result);
    }
}
