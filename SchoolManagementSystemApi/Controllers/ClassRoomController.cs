using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Model;
using SchoolManagementSystemApi.Services.StudentClass;
using SchoolManagementSystemApi.Utilities;

namespace SchoolManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = SD.SuperAdmin + "," + SD.Admin)]
    public class ClassRoomController : ControllerBase
    {
        public ClassRoomController(IClassRoom iClassRoom)
        {
            _iClassRoom=iClassRoom;
        }
        private IClassRoom _iClassRoom;
        /// <summary>
        ///     Register A Class.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// 



        [HttpPost("CreateClass")]
        [ProducesResponseType(typeof(ClassRoom), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateClass(ClassRoomDTO request)
        {
            _iClassRoom.CreateClass(request);
            return CreatedAtAction(nameof(GetClassById), request);
        }





        [HttpGet("GetAllClass")]
        [ProducesResponseType(typeof(ClassRoom), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllClass()
        {
            return Ok (await _iClassRoom.GetAllClass());
        }





        [HttpGet("GetClassById/{id}")]
        [ProducesResponseType(typeof(ClassRoom), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetClassById(Guid id)
        {
            var exsistingClass = await _iClassRoom.GetClassById(id);
            return exsistingClass == null ? NotFound() : Ok(exsistingClass);
        }



        [HttpPut("Update/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> EditClass(Guid id, ClassRoomDTO request)
        {
            await _iClassRoom.EditClass(id , request);
            return NoContent();
        }


        [HttpDelete("DeleteClass/id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteClass(Guid id)
        {
            try
            {
                await _iClassRoom.DeleteClass(id);
                return NoContent();
            }
            catch(ArgumentException ex)
            {
                return NotFound();
            }
        }




    }
}
