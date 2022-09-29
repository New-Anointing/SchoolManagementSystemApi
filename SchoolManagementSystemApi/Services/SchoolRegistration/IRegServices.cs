using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Model;

namespace SchoolManagementSystemApi.Services.SchoolRegistration
{
    public interface IRegServices
    {
        Task<ApplicationUser> SchoolRegistration(AdminUserDTO request);
        Task<ApplicationUser> UserRegistration(UserDTO request);
       
    }
}
