using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolManagementSystemApi.Data;
using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SchoolManagementSystemApi.Services.OrgRegistration
{
    public class RegServices : IRegServices
    {
        private readonly ApiDbContext _context;
        private IConfiguration _configuration;
        public static OrganisationRegistration user = new();
        public RegServices(ApiDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<OrganisationRegistration> SchoolRegistration(SchoolRegistrationDTO request)
        {
            var existingOrg = _context.OrganisationReg.FirstOrDefault(e=>e.Email == request.Email);
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

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }
        
        
        public async Task<string> Login(UserLoginDto request)
        {
            var loginUser = await _context.OrganisationReg.FirstOrDefaultAsync(o=>o.Email == request.EmailAddress);
            if(loginUser == null)
            {
                return null;
            }
  
            if(!VerifyPasswordHash(request.Password, loginUser.PasswordHash, loginUser.PasswordSalt))
            {
                return null;
            }
            user = loginUser;
            string token = CreateToken(user);
            return token;
        }

        private string CreateToken(OrganisationRegistration user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email)
            };
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("JwtTokens:Key").Value));
            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims : claims,
                expires : DateTime.Now.AddDays(1),
                signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

    }
}
