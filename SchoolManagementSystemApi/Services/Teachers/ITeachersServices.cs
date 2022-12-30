using SchoolManagementSystemApi.Helpers;
using SchoolManagementSystemApi.Model;

namespace SchoolManagementSystemApi.Services.Teachers
{
    public interface ITeachersServices
    {
        Task<GenericResponse<IEnumerable<ApplicationUser>>> GetAllTeachers(); 
    }
}
