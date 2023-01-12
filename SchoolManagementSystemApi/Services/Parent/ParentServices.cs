using SchoolManagementSystemApi.DTOModel;
using SchoolManagementSystemApi.Helpers;
using SchoolManagementSystemApi.Model;

namespace SchoolManagementSystemApi.Services.Parent
{
    public class ParentServices : IParentServices
    {

        public Task<GenericResponse<IEnumerable<ApplicationUser>>> GetAllParents()
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<Parents>> RegisterUserAsParents(ParentUserDTO result)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<IEnumerable<Parents>>> GetAllRegisteredParents()
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<Parents>> AddStudentsToParent(ParentStudentDTO result, Guid StudentId)
        {
            throw new NotImplementedException();
        }
    }
}
