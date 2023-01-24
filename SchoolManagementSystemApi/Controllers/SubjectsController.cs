using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Helpers;
using SchoolManagementSystemApi.Services.ClassSubjects;
using SchoolManagementSystemApi.Utilities;

namespace SchoolManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = SD.SuperAdmin + "," + SD.Admin)]

    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectsServices _subjectServices;
        public SubjectsController
        (
            ISubjectsServices subjectsServices
        )
        {
            _subjectServices = subjectsServices;
        }

        [HttpPost("CreateSubject")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GenericResponse<SubjectsDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<SubjectsDTO>))]
        public async Task<ActionResult> CreateSubject(SubjectsDTO request)
        {
            var result = await _subjectServices.CreateSubject(request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("GetAllSubject")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<IEnumerable<SubjectsDTO>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<IEnumerable<SubjectsDTO>>))]
        public async Task<ActionResult> GetAllSubject()
        {
            var result = await _subjectServices.GetAllSubject();
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("GetSubjectById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<SubjectsDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GenericResponse<SubjectsDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<SubjectsDTO>))]
        public async Task<ActionResult> GetSubjectById(Guid id)
        {
            var result = await _subjectServices.GetSubjectById(id);
            return StatusCode((int)result.StatusCode, result);
        }

    }
}
