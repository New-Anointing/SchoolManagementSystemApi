using SchoolManagementSystemApi.Data;
using System.Security.Claims;

namespace SchoolManagementSystemApi.Services.UserResolver
{
    public class UserResolverService : IUserResolverServices
    {
        private readonly ApiDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserResolverService
        (
            IHttpContextAccessor httpContextAccessor
,           ApiDbContext context

        )
        {
            _httpContextAccessor=httpContextAccessor;
            _context=context;
        }

        public Guid GetOrgId()
        {
            string claim = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                claim = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            var orgId = _context.ApplicationUser.FirstOrDefault(c => c.Id == claim).OrganisationId;

            return orgId;
        }
    }
}
