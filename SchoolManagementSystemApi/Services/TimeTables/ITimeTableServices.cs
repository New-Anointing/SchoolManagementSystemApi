
using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Helpers;
using SchoolManagementSystemApi.Model;

namespace SchoolManagementSystemApi.Services.TimeTables
{
    public interface ITimeTableServices
    {
        Task<GenericResponse<TimeTable>> CreateTimeTable(TimeTableDTO request);
        Task<GenericResponse<IEnumerable<TimeTable>>> GetAllTimeTimeTable();
        Task<GenericResponse<IEnumerable<TimeTable>>> GetTimeTableForClass(Guid classId);
        Task<GenericResponse<TimeTable>> DeleteTimeTable(Guid Id);

    }
}
