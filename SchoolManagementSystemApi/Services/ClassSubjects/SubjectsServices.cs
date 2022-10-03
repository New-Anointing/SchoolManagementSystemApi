using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemApi.Data;
using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Helpers;
using SchoolManagementSystemApi.Model;
using SchoolManagementSystemApi.Services.UserResolver;
using System.Net;

namespace SchoolManagementSystemApi.Services.ClassSubjects
{
    public class SubjectsServices : ISubjectsServices
    {
        private readonly ApiDbContext _context;
        private readonly IUserResolverServices _userResolverService;
        private static Subjects subject = new ();

        public SubjectsServices
        (
            ApiDbContext context,
            IUserResolverServices userResolverServices
        )
        {
            _context=context;
            _userResolverService = userResolverServices;
        }
        private Guid OrgId => _userResolverService.GetOrgId();

        public async Task<GenericResponse<Subjects>> CreateSubject(SubjectsDTO request)
        {
            try
            {
                subject.Id = Guid.NewGuid();
                subject.Subject = request.Subject;
                subject.OrganisationId = OrgId;
                await _context.Subjects.AddAsync(subject);
                await _context.SaveChangesAsync();
                return new GenericResponse<Subjects>
                {
                    StatusCode = HttpStatusCode.Created,
                    Data = subject,
                    Message = "Subject was created successfully",
                    Success = true
                };


            }
            catch(Exception e)
            {
                return new GenericResponse<Subjects>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null,
                    Message = e.Message,
                    Success = false
                };
            }
        }

        public async Task<GenericResponse<IEnumerable<Subjects>>> GetAllSubject()
        {
            try
            {
                var subjects = await _context.Subjects.Where(s => s.OrganisationId == OrgId && s.IsDeleted == false).ToListAsync();
                return new GenericResponse<IEnumerable<Subjects>>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = subjects,
                    Message = "Data loaded successfully",
                    Success = true
                };
            }
            catch(Exception e)
            {
                return new GenericResponse<IEnumerable<Subjects>>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null,
                    Message = "An error occurred: " + e.Message,
                    Success = false
                };
            }
        }

        public async Task<GenericResponse<Subjects>> GetSubjectById(Guid id)
        {
            try
            {
                subject = await _context.Subjects.FirstOrDefaultAsync(s => s.Id == id && s.OrganisationId == OrgId);
                if(subject == null)
                {
                    return new GenericResponse<Subjects>
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Data = null,
                        Message = "No Subject with this id exist :(",
                        Success = false
                    };
                }
                return new GenericResponse<Subjects>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = subject,
                    Message = "Data loaded successfully",
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new GenericResponse<Subjects>
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
