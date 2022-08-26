using SchoolManagementSystemApi.Data;
using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Model;
using System.Security.Cryptography;
using System.Text;

namespace SchoolManagementSystemApi.Services.OrgRegistration
{
    public class OrgRegServices : IOrgRegServices
    {
        private readonly ApiDbContext _context;
        public static OrganisationRegistration user = new();
        public OrgRegServices(ApiDbContext context)
        {
            _context = context;
        }
        public async Task<OrganisationRegistration> Register(OrganisationRegistrationDTO request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.Email = request.Email;
            user.SchoolName = request.SchoolName;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Address = request.Address;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;

             _context.OrganisationReg.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }


    }
}
