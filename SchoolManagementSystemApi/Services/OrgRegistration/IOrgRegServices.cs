using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Model;

namespace SchoolManagementSystemApi.Services.OrgRegistration
{
    public interface IOrgRegServices
    {
        Task<OrganisationRegistration> Register(OrganisationRegistrationDTO request);
    }
}
