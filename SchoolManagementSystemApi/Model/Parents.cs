namespace SchoolManagementSystemApi.Model
{
    public class Parents : BaseClass
    {
        public Guid Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual List<Students> StudentUser { get; set; } = new List<Students>();
    }
}
