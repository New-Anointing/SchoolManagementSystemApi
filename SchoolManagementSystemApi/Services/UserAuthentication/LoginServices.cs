﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolManagementSystemApi.Data;
using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Model;
using SchoolManagementSystemApi.Services.UserAuthorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SchoolManagementSystemApi.Services.UserAuthentication
{
    public class LoginServices : ILoginServices
    {
        private readonly ApiDbContext _context;
        private IConfiguration _configuration;
        private readonly UserManager<OrganisationRegistration> _userManager;

        public LoginServices
        (
            ApiDbContext context,
            IConfiguration configuration,
            UserManager<OrganisationRegistration> userManager
        )
        {
            _context = context;
            _configuration = configuration;
            _userManager = userManager;

        }




        public async Task<string> Login(UserLoginDto request)
        {
            var userExist = await _userManager.FindByEmailAsync(request.EmailAddress);
            if (userExist != null && await _userManager.CheckPasswordAsync(userExist, request.Password))            
            {

                var token = CreateToken(userExist).Result;
                return token;

            }
            throw new InvalidOperationException("Incorrect Passwoord");
        }

        private async Task<string> CreateToken(OrganisationRegistration user)
        {
            var userRoles =  await _userManager.GetRolesAsync(user);
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
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