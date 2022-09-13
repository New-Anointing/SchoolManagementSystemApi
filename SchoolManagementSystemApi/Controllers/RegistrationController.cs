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
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> SchoolRegistration(AdminUserDTO request)
        {
            try
            {
                await _iorgRegServices.SchoolRegistration(request);
            }
            catch(ArgumentException)
            {
                return Conflict();
            }
            return Created("", Ok("User Created Successfully"));
        }

    }
}
