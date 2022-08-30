using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Model;

namespace SchoolManagementSystemApi.Services.OrgRegistration
{
    public interface IRegServices
    {
        Task<OrganisationRegistration> SchoolRegistration(SchoolRegistrationDTO request);
        Task<string> Login(UserLoginDto request);
    }
}
