using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Helpers;

namespace SchoolManagementSystemApi.Services.UserAuthorization
{
    public interface ILoginServices
    {
         Task<GenericResponse<string>> Login(UserLoginDto request);
    }
}
