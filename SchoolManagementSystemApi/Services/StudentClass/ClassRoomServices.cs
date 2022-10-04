using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemApi.Data;
using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Helpers;
using SchoolManagementSystemApi.Model;
using SchoolManagementSystemApi.Services.UserResolver;
using System.Net;

namespace SchoolManagementSystemApi.Services.StudentClass
{
    public class ClassRoomServices : IClassRoomServices
    {
        private readonly ApiDbContext _context;
        private readonly IUserResolverServices _userResolverService;
        private static ClassRoom classRoom = new();

        public ClassRoomServices
        (
            ApiDbContext context,
            IUserResolverServices userResolverServices
        )
        {
            _context=context;
            _userResolverService = userResolverServices;
        }


        private Guid OrgId => _userResolverService.GetOrgId();

        public async Task<GenericResponse<ClassRoom>> CreateClass(ClassRoomDTO request)
        {
            try
            {
                classRoom.Class = request.Class;
                classRoom.ShortCode = request.ShortCode;
                classRoom.OrganisationId = OrgId;
                classRoom.Id =  Guid.NewGuid();
                await _context.ClassRoom.AddAsync(classRoom);
                await _context.SaveChangesAsync();
                return new GenericResponse<ClassRoom>
                {
                    StatusCode = HttpStatusCode.Created,
                    Data = classRoom,
                    Message = "Class was created successfully",
                    Success = true
                };
            }
            catch(Exception e)
            {
                return new GenericResponse<ClassRoom>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null,
                    Message = "An error occurred: " + e.Message,
                    Success = false
                };
            }
            

        }
        public async Task<GenericResponse<IEnumerable<ClassRoom>>> GetAllClass()
        {
            try
            {
                var classRooms = await _context.ClassRoom.Where(c => c.OrganisationId == OrgId && c.IsDeleted == false).ToListAsync();
                return new GenericResponse<IEnumerable<ClassRoom>>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = classRooms,
                    Message = "Data loaded successfully",
                    Success = true
                };
            }
            catch(Exception e)
            {
                return new GenericResponse<IEnumerable<ClassRoom>>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null,
                    Message = "An error occurred: " + e.Message,
                    Success = false
                };
            }
        }


        public async Task<GenericResponse<ClassRoom>> GetClassById(Guid id)
        {
            try
            {
                classRoom = await _context.ClassRoom.FirstOrDefaultAsync(c => c.Id == id && c.OrganisationId == OrgId);
                if(classRoom == null)
                {
                    return new GenericResponse<ClassRoom>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Data = null,
                        Message = "No Class with this id exist :(",
                        Success = false
                    };
                }
                return new GenericResponse<ClassRoom>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = classRoom,
                    Message = "Data loaded successfully",
                    Success = true
                };
            }
            catch(Exception e)
            {
                return new GenericResponse<ClassRoom>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null,
                    Message = "An error occurred: " + e.Message,
                    Success = false
                };
            }
        }

        //public async Task<GenericResponse<ClassRoom>> EditClass(Guid id, ClassRoomDTO request)
        //{

        //    var classToEdit = GetClassById(id).Result;
        //    if (classToEdit.Success == false)
        //    {
        //        return new GenericResponse<ClassRoom>
        //        {
        //            StatusCode = HttpStatusCode.NotFound,
        //            Data = null,
        //            Message = "No Class with this id exist :(",
        //            Success = false
        //        };
        //    }
        //    if(!string.IsNullOrEmpty(request.Class))
        //    {
        //        classToEdit.Data.Class = request.Class;
        //    }
        //    if (!string.IsNullOrEmpty(request.ShortCode))
        //    {
        //        classToEdit.Data.ShortCode = request.ShortCode;
        //    }
        //    classToEdit.Data.DateModified = DateTime.Now;
        //    _context.Update(classToEdit);
        //    await _context.SaveChangesAsync();
        //    return classToEdit;

        //}

    }
}
