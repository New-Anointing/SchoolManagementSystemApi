using SchoolManagementSystemApi.Model;

namespace SchoolManagementSystemApi.DTOModel
{
    public class ParentStudentDTO
    {
        public virtual List<Students> StudentUser { get; set; } = new List<Students>();
    }
}
