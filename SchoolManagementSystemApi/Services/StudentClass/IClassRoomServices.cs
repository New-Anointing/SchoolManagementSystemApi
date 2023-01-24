using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Helpers;
using SchoolManagementSystemApi.Model;

namespace SchoolManagementSystemApi.Services.StudentClass
{
    public interface IClassRoomServices
    {
        Task<GenericResponse<ClassRoom>> CreateClass(ClassRoomDTO request);
        //Task<GenericResponse<ClassRoom>> EditClass(Guid id, ClassRoomDTO request);
        Task<GenericResponse<IEnumerable<ClassRoom>>> GetAllClass();
        Task<GenericResponse<ClassRoom>> GetClassById(Guid id);
    }
}
