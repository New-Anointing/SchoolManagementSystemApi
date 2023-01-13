using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Helpers;
using SchoolManagementSystemApi.Model;

namespace SchoolManagementSystemApi.Services.Parent
{
    public interface IParentServices
    {
        Task<GenericResponse<IEnumerable<ApplicationUser>>> GetAllParents();
        Task<GenericResponse<Parents>> RegisterUserAsParents(ParentUserDTO result);
        Task<GenericResponse<IEnumerable<Parents>>> GetAllRegisteredParents();
        Task<GenericResponse<Parents>> AddStudentsToParent(ParentStudentDTO result, Guid ParentId);
    }
}
