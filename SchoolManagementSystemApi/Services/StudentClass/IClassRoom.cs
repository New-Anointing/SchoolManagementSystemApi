using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Model;

namespace SchoolManagementSystemApi.Services.StudentClass
{
    public interface IClassRoom
    {
        Task<ClassRoom> CreateClass(ClassRoomDTO request);
        Task<ClassRoom> EditClass(Guid id, ClassRoomDTO request);
        Task<IEnumerable<ClassRoom>> GetAllClass();
        Task<ClassRoom> GetClassById(Guid id);
        Task DeleteClass(Guid id);
    }
}
