using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Model;

namespace SchoolManagementSystemApi.Services.ClassSubjects
{
    public interface ISubjects
    {
        Task<Subjects> CreateClass(SubjectsDTO request);
        Task<Subjects> EditClass(Guid id, SubjectsDTO request);
        Task<IEnumerable<Subjects>> GetAllClass();
        Task<Subjects> GetClassById(Guid id);
        Task DeleteClass(Guid id);
    }
}
