using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SchoolManagementSystemApi.Data;
using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Helpers;
using SchoolManagementSystemApi.Model;
using SchoolManagementSystemApi.Services.UserAuthorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace SchoolManagementSystemApi.Services.UserAuthentication
{
    public class LoginServices : ILoginServices
    {
        private readonly ApiDbContext _context;
        private IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginServices
        (
            ApiDbContext context,
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager
        )
        {
            _context = context;
            _configuration = configuration;
            _userManager = userManager;

        }




        public async Task<GenericResponse<string>> Login(UserLoginDto request)
        {
            
            try
            {
                var userExist = await _userManager.FindByEmailAsync(request.EmailAddress);
                if (userExist != null && await _userManager.CheckPasswordAsync(userExist, request.Password))
                {

                    var token = CreateToken(userExist).Result;
                    return new GenericResponse<string>
                    {
                        StatusCode = HttpStatusCode.OK,
                        Data = token,
                        Message = "Login successful",
                        Success = true
                    };

                }
                return new GenericResponse<string>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Data = null,
                    Message = "Incorrect Passwoord Or User Doesn't Exist",
                    Success = false
                };
            }
            catch (Exception e)
            {
                return new GenericResponse<string>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null,
                    Message = "An error occurred: " + e.Message,
                    Success = false
                };
            }
        }



        private async Task<string> CreateToken(ApplicationUser user)
        {
            var userRoles =  await _userManager.GetRolesAsync(user);
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };
            foreach(var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("JwtTokens:Key").Value));
            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
