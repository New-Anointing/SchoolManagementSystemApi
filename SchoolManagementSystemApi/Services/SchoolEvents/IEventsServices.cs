using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Helpers;
using SchoolManagementSystemApi.Model;

namespace SchoolManagementSystemApi.Services.SchoolEvents
{
    public interface IEventsServices
    {
        Task<GenericResponse<Events>> CreateEvent(EventsDTO request);
        Task<GenericResponse<IEnumerable<Events>>> GetAllEvents();
        Task<GenericResponse<Events>> GetEventById(Guid id);
    }
}
