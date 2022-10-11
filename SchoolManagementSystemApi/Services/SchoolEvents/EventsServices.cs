using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemApi.Data;
using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Helpers;
using SchoolManagementSystemApi.Model;
using SchoolManagementSystemApi.Services.UserResolver;
using System.Net;

namespace SchoolManagementSystemApi.Services.SchoolEvents
{
    public class EventsServices : IEventsServices
    {
        private readonly ApiDbContext _context;
        private readonly IUserResolverServices _userResolverServices;
        private static Events _event = new();

        public EventsServices
        (
            ApiDbContext context,
            IUserResolverServices userResolverServices
        )
        {
            _context=context;
            _userResolverServices=userResolverServices;
        }

        private Guid OrgId => _userResolverServices.GetOrgId();

        public async Task<GenericResponse<Events>> CreateEvent(EventsDTO request)
        {
            try
            {
                _event.Id  = Guid.NewGuid();
                _event.Name = request.Name;
                _event.EventDateTime = request.EventDateTime;
                _event.OrganisationId = OrgId;
                await _context.Events.AddAsync(_event);
                await _context.SaveChangesAsync();
                return new GenericResponse<Events>
                {
                    StatusCode = HttpStatusCode.Created,
                    Data = _event,
                    Message = "Event created successfully",
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new GenericResponse<Events>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null,
                    Message = "An error occurred: " + e.Message,
                    Success = false
                };
            }
        }

        public async Task<GenericResponse<IEnumerable<Events>>> GetAllEvents()
        {
            try
            {
                var events = await _context.Events.Where(e => e.OrganisationId == OrgId).ToListAsync();
                return new GenericResponse<IEnumerable<Events>>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = events,
                    Message = "events loaded successfully",
                    Success = true
                };
            }
            catch(Exception e)
            {
                return new GenericResponse<IEnumerable<Events>>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null,
                    Message = "An error occurred: " + e.Message,
                    Success = false
                };
            }
        }

        public async Task<GenericResponse<Events>> GetEventById(Guid id)
        {
            try
            {
                var newEvent = await _context.Events.FirstOrDefaultAsync(e => e.Id == id && e.OrganisationId == OrgId);
                if(newEvent == null)
                {
                    return new GenericResponse<Events>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Data = null,
                        Message = "No Event with this id exist",
                        Success = false
                    };
                }
                return new GenericResponse<Events>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = newEvent,
                    Message = "Data loaded succesfully",
                    Success =  true
                };
            }
            catch(Exception e)
            {
                return new GenericResponse<Events>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null,
                    Message = "An error occurred: " + e.Message,
                    Success = false
                };
            }
            throw new NotImplementedException();
        }
    }
}
