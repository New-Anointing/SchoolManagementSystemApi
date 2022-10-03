using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Helpers;
using SchoolManagementSystemApi.Model;

namespace SchoolManagementSystemApi.Services.ClassSubjects
{
    public interface ISubjectsServices
    {
        Task<GenericResponse<Subjects>> CreateSubject(SubjectsDTO request);
       // Task<GenericResponse<Subjects>> EditClass(Guid id, SubjectsDTO request);
        Task<GenericResponse<IEnumerable<Subjects>>> GetAllSubject();
        Task<GenericResponse<Subjects>> GetSubjectById(Guid id);
    }
}
