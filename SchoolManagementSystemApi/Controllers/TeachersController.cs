using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Helpers;
using SchoolManagementSystemApi.Model;
using SchoolManagementSystemApi.Services.Teacher;
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

        [HttpGet("GetAllUserWithTeacherRoles")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<IEnumerable<ApplicationUser>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<IEnumerable<ApplicationUser>>))]
        public async Task<ActionResult> GetAllTeachers()
        {
            var result = await _iTeachersServices.GetAllTeachers();
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost("RegisterUsersAsTeachers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<Teachers>))]
        [ProducesResponseType(StatusCodes.Status417ExpectationFailed, Type = typeof(GenericResponse<Teachers>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<Teachers>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse<Teachers>))]
        public async Task<ActionResult> RegisterTeachers(TeacherUserDTO request)
        {
            var result = await _iTeachersServices.RegisterTeachers(request);
            return StatusCode((int)result.StatusCode, result);

        }

        [HttpGet("GetAllRegisteredTeachers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<IEnumerable<Teachers>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GenericResponse<IEnumerable<Teachers>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<IEnumerable<Teachers>>))]
        public async Task<ActionResult> GetAllRegisteredTeachers()
        {
            var result = await _iTeachersServices.GetAllRegisteredTeachers();
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut("AssignClassTeachers/{TeacherId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<Teachers>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<Teachers>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GenericResponse<Teachers>))]
        public async Task<ActionResult> AssignClassTeachers(ClassTeacherDTO request, Guid TeacherId)
        {
            var result = await _iTeachersServices.AssignClassTeachers(request, TeacherId);
            return StatusCode((int)result.StatusCode, result);

        }
    }
}
