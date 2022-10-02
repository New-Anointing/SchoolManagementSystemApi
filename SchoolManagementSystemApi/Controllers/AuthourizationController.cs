using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Helpers;
using SchoolManagementSystemApi.Model;
using SchoolManagementSystemApi.Services.UserAuthorization;

namespace SchoolManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthourizationController : ControllerBase
    {
        private readonly ILoginServices _loginServices;
        public AuthourizationController
        (
            ILoginServices loginServices
        )
        {
            _loginServices = loginServices;
        }
        /// <summary>
        /// Signin A User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse<string>))]
        public async Task<IActionResult> Login(UserLoginDto request)
        {
            var result = await _loginServices.Login(request);
            return StatusCode((int)result.StatusCode, result);


        }
    }
}
