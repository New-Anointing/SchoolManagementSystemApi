using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Services.OrgRegistration;

namespace SchoolManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrgRegistrationController : ControllerBase
    {
        private IOrgRegServices _iorgRegServices;
        public OrgRegistrationController(IOrgRegServices iorgRegServices)
        {
            _iorgRegServices = iorgRegServices;
        }
        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Register(OrganisationRegistrationDTO request)
        {
            await _iorgRegServices.Register(request);
            return Ok();
        }
    }
}
