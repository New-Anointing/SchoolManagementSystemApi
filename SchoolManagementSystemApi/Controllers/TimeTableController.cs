using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Helpers;
using SchoolManagementSystemApi.Services.TimeTables;
using SchoolManagementSystemApi.Utilities;

namespace SchoolManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = SD.SuperAdmin + "," + SD.Admin)]
    public class TimeTableController : ControllerBase
    {
        private readonly ITimeTableServices _timeTableServices;

        public TimeTableController
        (
            ITimeTableServices timeTableServices
        )
        {
            _timeTableServices=timeTableServices;
        }

        [HttpPost("CreateTimeTable")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GenericResponse<TimeTableDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<TimeTableDTO>))]
        public async Task<ActionResult> CreateTimeTable(TimeTableDTO request)
        {
            var result = await _timeTableServices.CreateTimeTable(request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("GetAllTimeTimeTable")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<IEnumerable<TimeTableDTO>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<IEnumerable<TimeTableDTO>>))]
        public async Task<ActionResult> GetAllTimeTimeTable()
        {
            var result = await _timeTableServices.GetAllTimeTimeTable();
            return StatusCode((int)result.StatusCode, result);
        }


        [HttpGet("CreateTimeTable/{classId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<IEnumerable<TimeTableDTO>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<IEnumerable<TimeTableDTO>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GenericResponse<IEnumerable<TimeTableDTO>>))]
        public async Task<ActionResult> GetTimeTableForClass(Guid classId)
        {
            var result = await _timeTableServices.GetTimeTableForClass(classId);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
