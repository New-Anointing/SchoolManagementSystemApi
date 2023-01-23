﻿using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemApi.Data;
using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Helpers;
using SchoolManagementSystemApi.Model;
using SchoolManagementSystemApi.Services.UserResolver;
using SchoolManagementSystemApi.Utilities;
using System.Net;

namespace SchoolManagementSystemApi.Services.Parent
{
    public class ParentServices : IParentServices
    {
        private readonly ApiDbContext _context;
        private readonly IUserResolverServices _userResolverServices;
        private static Parents _parents = new();
        public ParentServices
        (
            ApiDbContext context,
            IUserResolverServices userResolverServices
        )
        {
            _context = context;
            _userResolverServices = userResolverServices;
        }
        private Guid OrgId => _userResolverServices.GetOrgId();

        public async Task<GenericResponse<IEnumerable<ApplicationUser>>> GetAllParents()
        {
            try
            {
                var userWithParentRole = await _context.ApplicationUser.Where(p=>p.OrganisationId== OrgId && p.Role == SD.Parent).ToListAsync();
                return new GenericResponse<IEnumerable<ApplicationUser>>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = userWithParentRole,
                    Message = "Data loaded successfully",
                    Success = true
                };

            }
            catch (Exception e)
            {
                return new GenericResponse<IEnumerable<ApplicationUser>>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null,
                    Message = "An error occurred: " + e.Message,
                    Success = false
                };
            }
        }

        public async Task<GenericResponse<Parents>> RegisterUserAsParents(ParentUserDTO result)
        {
            try
            {
                var User = await _context.ApplicationUser.FirstOrDefaultAsync(a => a.OrganisationId == OrgId && a.Id == result.ApplicationUserId.ToString());
                if (User == null)
                {
                    return new GenericResponse<Parents>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Data = null,
                        Message = "No user with this id exist",
                        Success = false
                    };
                }
                var isAParent = await _context.Parents.FirstOrDefaultAsync(t => t.OrganisationId == OrgId && t.ApplicationUser.Id == result.ApplicationUserId.ToString() && t.IsDeleted == false);
                if (isAParent == null)
                {
                    _parents.Id = Guid.NewGuid();
                    _parents.OrganisationId = OrgId;
                    _parents.ApplicationUser = User;
                    await _context.Parents.AddAsync(_parents);
                    await _context.SaveChangesAsync();
                    return new GenericResponse<Parents>
                    {
                        StatusCode = HttpStatusCode.OK,
                        Data = _parents,
                        Message = "User reistered successfully",
                        Success = true
                    };
                    
                }
                return new GenericResponse<Parents>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Data = null,
                    Message = "User has been previously reistered as a Parent",
                    Success = false
                };       
            }
            catch(Exception e) 
            {
                return new GenericResponse<Parents>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null,
                    Message = "An error occurred: " + e.Message,
                    Success = false
                };
            }
        }

        public async Task<GenericResponse<IEnumerable<Parents>>> GetAllRegisteredParents()
        {
            try
            {
                var regParents = await _context.Parents.Include(p=>p.ApplicationUser).Include(p=>p.StudentUser).ToListAsync();
                if(regParents != null)
                {
                    return new GenericResponse<IEnumerable<Parents>>
                    {
                        StatusCode = HttpStatusCode.OK,
                        Data = regParents,
                        Message = "Data loaded successfully",
                        Success = true
                    };
                }

                return new GenericResponse<IEnumerable<Parents>>
                {
                    StatusCode = HttpStatusCode.NoContent,
                    Data = null,
                    Message = "No teacher have been registered",
                    Success = false
                };
            }
            catch(Exception e)
            {
                return new GenericResponse<IEnumerable<Parents>>()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null,
                    Message = $"An error occured: {e.Message}",
                    Success = false
                };
            }
        }

        public async Task<GenericResponse<Parents>> AddStudentsToParent(ParentStudentDTO result, Guid ParentId)
        {
            try
            {
                List<Students> students = new();
                foreach (var student in result.StudentUser)
                {
                    students.Add(await _context.Students.Include(s=>s.ApplicationUser).Include(s=>s.Subjects).FirstOrDefaultAsync(s=>s.Id == student && s.OrganisationId == OrgId));
                }
                if (students.Contains(null))
                {
                    return new GenericResponse<Parents>
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Data = null,
                        Message = "Invalid request. One or more student does not exist",
                        Success = false
                    };
                }
                var parent = await _context.Parents.Include(p => p.ApplicationUser).Include(p => p.StudentUser).FirstOrDefaultAsync(p => p.OrganisationId == OrgId && p.IsDeleted == false && p.Id == ParentId);
                if (parent != null)
                {
                    parent.StudentUser.Clear();
                    parent.StudentUser = students;
                    await _context.SaveChangesAsync();
                    return new GenericResponse<Parents>
                    {
                        StatusCode = HttpStatusCode.OK,
                        Data = parent,
                        Message = "Parent student(s) have been reistered",
                        Success = true
                    };
                }

                return new GenericResponse<Parents>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Data = null,
                    Message = "No Parent with this id exist",
                    Success = false
                };
            }
            catch(Exception e)
            {
                return new GenericResponse<Parents>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null,
                    Message = "An error occurred: " + e.Message,
                    Success = false
                };
            }
        }

        public async Task<GenericResponse<Parents>> GetRegisteredParentById(Guid ParentId)
        {
            try
            {
                var parents = await _context.Parents.Include(p=>p.ApplicationUser).Include(p=>p.StudentUser).FirstOrDefaultAsync(p=>p.OrganisationId == OrgId && p.IsDeleted == false && p.Id == ParentId);
                if (parents != null)
                {
                    return new GenericResponse<Parents>
                    {
                        StatusCode = HttpStatusCode.OK,
                        Data = parents,
                        Message = "Data loaded successfully",
                        Success = true
                    };
                }
                return new GenericResponse<Parents>
                {
                    StatusCode =  HttpStatusCode.NotFound,
                    Data = null,
                    Message = "No registered parent with this id was found",
                    Success = false
                };
            }
            catch(Exception e)
            {
                return new GenericResponse<Parents>
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Data = null,
                    Message = $"An error occurred {e.Message}",
                    Success = false
                };
            }
        }
    }
}
