using SchoolManagementSystemApi.DTOModel;

namespace SchoolManagementSystemApi.Services.UserAuthorization
{
    public interface ILoginServices
    {
         Task<string> Login(UserLoginDto request);
    }
}
