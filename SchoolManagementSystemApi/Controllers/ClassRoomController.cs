using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Helpers;
using SchoolManagementSystemApi.Services.StudentClass;
using SchoolManagementSystemApi.Utilities;

namespace SchoolManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = SD.SuperAdmin + "," + SD.Admin)]
    public class ClassRoomController : ControllerBase
    {
        private readonly IClassRoomServices _iClassRoom;

        public ClassRoomController(IClassRoomServices iClassRoom)
        {
            _iClassRoom=iClassRoom;
        }
        /// <summary>
        ///     Register A Class.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// 



        [HttpPost("CreateClass")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GenericResponse<ClassRoomDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<ClassRoomDTO>))]
        public async Task<ActionResult> CreateClass(ClassRoomDTO request)
        {
            var result = await _iClassRoom.CreateClass(request);
            return StatusCode((int)result.StatusCode, result);
        }





        [HttpGet("GetAllClass")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<IEnumerable<ClassRoomDTO>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<IEnumerable<ClassRoomDTO>>))]
        public async Task<ActionResult> GetAllClass()
        {
            var result = await _iClassRoom.GetAllClass();
            return StatusCode((int)result.StatusCode, result);
        }





        [HttpGet("GetClassById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResponse<ClassRoomDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GenericResponse<ClassRoomDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GenericResponse<ClassRoomDTO>))]
        public async Task<ActionResult> GetClassById(Guid id)
        {
            var result = await _iClassRoom.GetClassById(id);
            return StatusCode((int)result.StatusCode, result);
        }



        //[HttpPut("EditClass/{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult> EditClass(Guid id, ClassRoomDTO request)
        //{
        //    await _iClassRoom.EditClass(id , request);
        //    return NoContent();
        //}
    }
}
