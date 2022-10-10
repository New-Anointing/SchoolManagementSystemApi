using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemApi.Data;
using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Helpers;
using SchoolManagementSystemApi.Model;
using SchoolManagementSystemApi.Services.UserResolver;
using System.Net;

namespace SchoolManagementSystemApi.Services.TimeTables
{
    public class TimeTableServices : ITimeTableServices
    {
        private readonly ApiDbContext _context;
        private readonly IUserResolverServices _userResolverService;
        private static TimeTable timeTable = new();

        public TimeTableServices
        (
            ApiDbContext context,
            IUserResolverServices userResolverServices
        )
        {
            _context=context;
            _userResolverService = userResolverServices;
        }

        private Guid OrgId => _userResolverService.GetOrgId();

        public async Task<GenericResponse<TimeTable>> CreateTimeTable(TimeTableDTO request)
        {
            try
            {
                var subject = _context.Subjects.FirstOrDefault(c => c.Id == request.SubjectId && c.OrganisationId == OrgId);
                if (subject == null)
                    return new GenericResponse<TimeTable>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Data = null,
                        Message = "The subject does not exist",
                        Success = false
                    };

                var classroom = _context.ClassRoom.FirstOrDefault(c => c.Id == request.ClassId && c.OrganisationId == OrgId);
                if (classroom == null)
                    return new GenericResponse<TimeTable>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Data = null,
                        Message = "The classroom does not exist",
                        Success = false
                    };

                timeTable.Id = Guid.NewGuid();
                timeTable.Subjects = subject;
                timeTable.ClassRoom = classroom;
                timeTable.TimeSchedule = request.TimeSchedule;
                timeTable.OrganisationId = OrgId;
                await _context.AddAsync(timeTable);
                await _context.SaveChangesAsync();
                return new GenericResponse<TimeTable>
                {
                    StatusCode = HttpStatusCode.Created,
                    Data = timeTable,
                    Message = "Time table slot was created",
                    Success = true
                };
            }
            catch(Exception e)
            {
                return new GenericResponse<TimeTable>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null,
                    Message = "An error occurred: " + e.Message,
                    Success = false
                };
            }
        }

        public async Task<GenericResponse<IEnumerable<TimeTable>>> GetAllTimeTimeTable()
        {
            try
            {
                var TimeTables = await _context.TimeTable.Include(x=>x.ClassRoom).Include(x=>x.Subjects).Where(c => c.OrganisationId == OrgId).ToListAsync();
                return new GenericResponse<IEnumerable<TimeTable>>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = TimeTables,
                    Message = "Data loaded successfully",
                    Success = true

                };
            }
            catch (Exception e)
            {
                return new GenericResponse<IEnumerable<TimeTable>>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null,
                    Message = "An error occurred: " + e.Message,
                    Success = false
                };
            }
        }

        public async Task<GenericResponse<IEnumerable<TimeTable>>> GetTimeTableForClass(Guid classId)
        {
            try
            {
                var timeTables = await _context.TimeTable.Include(x => x.ClassRoom).Include(x => x.Subjects).Where(c => c.OrganisationId == OrgId && c.ClassRoom.Id == classId).ToListAsync();
                if (!timeTables.Any())
                {
                    return new GenericResponse<IEnumerable<TimeTable>>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Data = null,
                        Message = "No timetable for this class exist",
                        Success = false
                    };
                }
                return new GenericResponse<IEnumerable<TimeTable>>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = timeTables,
                    Message = "Data Loaded Successfully!",
                    Success = true
                };
            }
            catch(Exception e)
            {
                return new GenericResponse<IEnumerable<TimeTable>>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null,
                    Message = "An error occurred: " + e.Message,
                    Success = false
                };
            }
        }
    }
}
