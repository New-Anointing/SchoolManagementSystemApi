using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Helpers;
using SchoolManagementSystemApi.Model;
using SchoolManagementSystemApi.Services.Parent;
using SchoolManagementSystemApi.Utilities;

namespace SchoolManagementSystemApi.Controllers
{
    [Authorize(Roles = $"{SD.Admin},{SD.SuperAdmin}")]
    [Route("api/[controller]")]
    [ApiController]
    public class ParentController : ControllerBase
    {
        private readonly IParentServices _iparentServices;
        public ParentController(IParentServices iparentServices)
        {
            _iparentServices=iparentServices;
        }

        [HttpGet("GetUsersWithParentRoles")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<IEnumerable<Parents>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<IEnumerable<Parents>>))]
        public async Task<ActionResult> GetAllParents()
        {
            var result = await _iparentServices.GetAllParents();
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost("RegisterUserAsParents")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<Parents>))]
        [ProducesResponseType(StatusCodes.Status417ExpectationFailed, Type = typeof(GenericResponse<Parents>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse<Parents>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<Parents>))]
        public async Task<ActionResult> RegisterUserAsParents(ParentUserDTO request)
        {
            var result = await _iparentServices.RegisterUserAsParents(request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost("GetAllRegisteredParents")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<IEnumerable<Parents>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GenericResponse<IEnumerable<Parents>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<IEnumerable<Parents>>))]
        public async Task<ActionResult> GetAllRegisteredParents()
        {
            var result = await _iparentServices.GetAllRegisteredParents();
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut("AssignStudentsToParents/{ParentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<Parents>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<Parents>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GenericResponse<Parents>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GenericResponse<Parents>))]
        public async Task<ActionResult> AddStudentsToParent(ParentStudentDTO request, Guid ParentId)
        {
            var result = await _iparentServices.AddStudentsToParent(request, ParentId); 
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("GetRegisteredParentById/{ParentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<Parents>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GenericResponse<Parents>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<Parents>))]
        public async Task<ActionResult> GetRegisteredParentById(Guid ParentId)
        {
            var result = await _iparentServices.GetRegisteredParentById(ParentId);
            return StatusCode((int)result.StatusCode, result);
        }

    }
}
