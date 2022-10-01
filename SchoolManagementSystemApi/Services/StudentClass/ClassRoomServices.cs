using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemApi.Data;
using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Model;
using System.Security.Claims;

namespace SchoolManagementSystemApi.Services.StudentClass
{
    public class ClassRoomServices : IClassRoom
    {
        private readonly ApiDbContext _context;
        private static ClassRoom classRoom = new();
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClassRoomServices(
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

        public async Task<ClassRoom> CreateClass(ClassRoomDTO request)
        {
            classRoom.Class = request.Class;
            classRoom.ShortCode = request.ShortCode;
            classRoom.OrganisationId = GetOrg();
            await _context.ClassRoom.AddAsync(classRoom);
            await _context.SaveChangesAsync();
            return classRoom;

        }
        public async Task<IEnumerable<ClassRoom>> GetAllClass()
        {
            var classRooms = await _context.ClassRoom.Where(c => c.OrganisationId == GetOrg()).ToListAsync();
            return classRooms;
        }


        public async Task<ClassRoom> GetClassById(Guid id)
        {
            return await _context.ClassRoom.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ClassRoom> EditClass(Guid id, ClassRoomDTO request)
        {

            var classToEdit = GetClassById(id).Result;
            if(!string.IsNullOrEmpty(request.Class))
            {
                classToEdit.Class = request.Class;
            }
            if (!string.IsNullOrEmpty(request.ShortCode))
            {
                classToEdit.ShortCode = request.ShortCode;
            }
            classToEdit.DateModified = DateTime.Now;
            _context.Update(classToEdit);
            await _context.SaveChangesAsync();
            return classToEdit;

        }
        public async Task DeleteClass(Guid id)
        {
            var classRoomToDelete = await _context.ClassRoom.FindAsync(id);
            if(classRoomToDelete == null)
            {
                throw new ArgumentException("No Class with this given id exist", nameof(id));
            }
            _context.ClassRoom.Remove(classRoomToDelete);
            await _context.SaveChangesAsync();
        }

    }
}
