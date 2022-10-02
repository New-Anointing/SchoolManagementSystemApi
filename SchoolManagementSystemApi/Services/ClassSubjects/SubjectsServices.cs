using SchoolManagementSystemApi.Data;
using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Model;
using System.Security.Claims;

namespace SchoolManagementSystemApi.Services.ClassSubjects
{
    public class SubjectsServices : ISubjects
    {
        private readonly ApiDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SubjectsServices(
            ApiDbContext context,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _context=context;
            _httpContextAccessor=httpContextAccessor;
        }
        private Guid GetOrg()
        {
            string claim = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                claim = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            var orgId = _context.ApplicationUser.Where(c => c.Id == claim).FirstOrDefault().OrganisationId;

            return orgId;
        }
        public Task<Subjects> CreateClass(SubjectsDTO request)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Subjects>> GetAllClass()
        {
            throw new NotImplementedException();
        }

        public Task<Subjects> GetClassById(Guid id)
        {
            throw new NotImplementedException();
        }
        public Task<Subjects> EditClass(Guid id, SubjectsDTO request)
        {
            throw new NotImplementedException();
        }
        public Task DeleteClass(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
