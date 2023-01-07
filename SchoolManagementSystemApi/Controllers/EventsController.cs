using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Helpers;
using SchoolManagementSystemApi.Services.SchoolEvents;
using SchoolManagementSystemApi.Utilities;

namespace SchoolManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = SD.SuperAdmin + "," + SD.Admin)]
    public class EventsController : ControllerBase
    {
        private readonly IEventsServices _iEventServices;

        public EventsController
        (
            IEventsServices iEventServices
        )
        {
            _iEventServices=iEventServices;
        }

        [HttpPost("CreateEvent")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GenericResponse<EventsDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<EventsDTO>))]
        public async Task<ActionResult> CreateEvent(EventsDTO request)
        {
            var result = await _iEventServices.CreateEvent(request);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("GetAllEvents")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GenericResponse<IEnumerable<EventsDTO>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<IEnumerable<EventsDTO>>))]
        public async Task<ActionResult> GetAllEvents()
        {
            var result = await _iEventServices.GetAllEvents();
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("GetEventById/{id}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GenericResponse<EventsDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GenericResponse<EventsDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<EventsDTO>))]
        public async Task<ActionResult> GetEventById(Guid id)
        {
            var result = await _iEventServices.GetEventById(id);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete("DeleteEvent/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GenericResponse<EventsDTO>))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(GenericResponse<EventsDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<EventsDTO>))]
        public async Task<ActionResult> DeleteEvent(Guid id)
        {
            var result = await _iEventServices.DeleteEvent(id);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut("EditEvent/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GenericResponse<EventsDTO>))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<EventsDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<EventsDTO>))]
        public async Task<ActionResult> EditEvent(Guid id, EventsDTO request)
        {
            var result = await _iEventServices.EditEvent(id, request);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}

