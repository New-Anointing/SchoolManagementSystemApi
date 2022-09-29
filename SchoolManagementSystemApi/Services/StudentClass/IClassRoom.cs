using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Model;

namespace SchoolManagementSystemApi.Services.StudentClass
{
    public interface IClassRoom
    {
        Task<ClassRoomServices> CreateClass(ClassRoomDTO request);
    }
}
