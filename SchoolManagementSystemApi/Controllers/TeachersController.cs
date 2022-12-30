using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemApi.Helpers;
using SchoolManagementSystemApi.Model;
using SchoolManagementSystemApi.Services.Teachers;
using SchoolManagementSystemApi.Utilities;

namespace SchoolManagementSystemApi.Controllers
{
    [Authorize(Roles = SD.Admin +","+ SD.SuperAdmin)]
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ITeachersServices _iTeachersServices;
        public TeachersController(ITeachersServices iTeachersServices)
        {
            _iTeachersServices=iTeachersServices;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<IEnumerable<ApplicationUser>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<IEnumerable<ApplicationUser>>))]
        public async Task<ActionResult> GetAllTeachers()
        {
            var result = await _iTeachersServices.GetAllTeachers();
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
