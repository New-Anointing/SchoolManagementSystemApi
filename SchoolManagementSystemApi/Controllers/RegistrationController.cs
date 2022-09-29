using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Services.SchoolRegistration;
using SchoolManagementSystemApi.Utilities;

namespace SchoolManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private IRegServices _iRegServices;
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> SchoolRegistration(AdminUserDTO request)
        {
            try
            {
                await _iRegServices.SchoolRegistration(request);
            }
            catch(ArgumentException)
            {
                return Conflict();
            }
            return Created("", Ok("User Created Successfully"));
        }
        [HttpPost("UsersRegistration")]
        [Authorize(Roles = SD.Admin +","+ SD.SuperAdmin)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> UserRegistration(UserDTO request)
        {
            try
            {
                await _iRegServices.UserRegistration(request);
            }
            catch(ArgumentException)
            {
                return Conflict();
            }
            return Created("", Ok("User Created Successfully"));
        }

    }
}
