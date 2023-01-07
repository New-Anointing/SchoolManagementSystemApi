using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Helpers;
using SchoolManagementSystemApi.Model;
using SchoolManagementSystemApi.Services.SchoolRegistration;
using SchoolManagementSystemApi.Utilities;

namespace SchoolManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegServices _iRegServices;
        public RegistrationController(IRegServices iRegServices)
        {
            _iRegServices = iRegServices;
        }
        /// <summary>
        ///     Register A School.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("SchoolRegistration")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GenericResponse<ApplicationUser>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<ApplicationUser>))]
        [ProducesResponseType(StatusCodes.Status417ExpectationFailed, Type = typeof(GenericResponse<ApplicationUser>))]
        public async Task<ActionResult> SchoolRegistration(AdminUserDTO request)
        {
            var result = await _iRegServices.SchoolRegistration(request);
            return StatusCode((int)result.StatusCode, result);

        }
        [HttpPost("UsersRegistration")]
        [Authorize(Roles = SD.Admin +","+ SD.SuperAdmin)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GenericResponse<ApplicationUser>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<ApplicationUser>))]
        [ProducesResponseType(StatusCodes.Status417ExpectationFailed, Type = typeof(GenericResponse<ApplicationUser>))]
        public async Task<ActionResult> UserRegistration(UserDTO request)
        {
            var result = await _iRegServices.UserRegistration(request);
            return StatusCode((int)result.StatusCode, result);

        }

        [HttpGet("GetAllUsers")]
        [Authorize(Roles = SD.Admin +","+ SD.SuperAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<IEnumerable<ApplicationUser>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type =  typeof(GenericResponse<IEnumerable<ApplicationUser>>))]
        public async Task<ActionResult> GetAllUsers()
        {
            var result = await _iRegServices.GetAllUsers();
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("GetUserById")]
        [Authorize(Roles = SD.Admin +","+ SD.SuperAdmin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<ApplicationUser>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type=typeof(GenericResponse<ApplicationUser>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GenericResponse<ApplicationUser>))]   
        public async Task<ActionResult> GetUserById(string id)
        {
            var result = await _iRegServices.GetUserById(id);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
