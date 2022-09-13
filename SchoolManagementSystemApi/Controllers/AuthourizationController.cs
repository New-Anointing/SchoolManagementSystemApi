using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Services.UserAuthorization;

namespace SchoolManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthourizationController : ControllerBase
    {
        private ILoginServices _loginServices;
        public AuthourizationController
        (
            ILoginServices loginServices
        )
        {
            _loginServices = loginServices;
        }
        [HttpPost("Login")]
        public IActionResult Login(UserLoginDto request)
        {
            try
            {
               // await _loginServices.Login(request);
            return Ok(new { Value = _loginServices.Login(request) });
            }
            catch (ArgumentException)
            {
                return NotFound("User not found or incorrect password");
            }



        }
    }
}
