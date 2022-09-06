using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Services.SchoolRegistration;

namespace SchoolManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private IRegServices _iorgRegServices;
        public RegistrationController(IRegServices iorgRegServices)
        {
            _iorgRegServices = iorgRegServices;
        }
        [HttpPost("SchoolRegistration")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> SchoolRegistration(SchoolRegistrationDTO request)
        {
            await _iorgRegServices.SchoolRegistration(request);
            return Created("", Ok());
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto request)
        {
            return await _iorgRegServices.Login(request) == null ? NotFound("User not found or incorrect password") : Ok(new {Value = _iorgRegServices.Login(request)});


        }
    }
}
