using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Helpers;
using SchoolManagementSystemApi.Model;
using SchoolManagementSystemApi.Services.Student;
using SchoolManagementSystemApi.Utilities;

namespace SchoolManagementSystemApi.Controllers
{
    [Authorize(Roles = SD.Admin +","+ SD.SuperAdmin)]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsServices _iStudentsServices;
        public StudentsController(IStudentsServices iStudentsServices)
        {
            _iStudentsServices=iStudentsServices;
        }

        [HttpGet("GetUserWithStudentsRoles")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<IEnumerable<Students>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<IEnumerable<Students>>))]
        public async Task<ActionResult> GetAllStudents()
        {
            var result = await _iStudentsServices.GetAllStudents();
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost("RegisterUserAsStudents")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<Students>))]
        [ProducesResponseType(StatusCodes.Status417ExpectationFailed, Type = typeof(GenericResponse<Students>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<Students>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse<Students>))]
        public async Task<ActionResult> RegisterStudents(StudentUserDTO request)
        {
            var result = await _iStudentsServices.RegisterStudents(request);
            return StatusCode((int)result.StatusCode, result);

        }

        [HttpGet("GetAllRegisteredStudents")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<IEnumerable<Students>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GenericResponse<IEnumerable<Students>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<IEnumerable<Students>>))]
        public async Task<ActionResult> GetAllRegisteredStudents()
        {
            var result = await _iStudentsServices.GetAllRegisteredStudents();
            return StatusCode((int)result.StatusCode, result);
        }

    }
}
