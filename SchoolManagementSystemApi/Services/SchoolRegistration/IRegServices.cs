using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Helpers;
using SchoolManagementSystemApi.Model;

namespace SchoolManagementSystemApi.Services.SchoolRegistration
{
    public interface IRegServices
    {
        Task<GenericResponse<ApplicationUser>> SchoolRegistration(AdminUserDTO request);
        Task<GenericResponse<ApplicationUser>> UserRegistration(UserDTO request);
        Task<GenericResponse<IEnumerable<ApplicationUser>>> GetAllUsers();
        Task<GenericResponse<ApplicationUser>> GetUserById(string id);
       
    }
}
