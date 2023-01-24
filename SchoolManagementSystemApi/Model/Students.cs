namespace SchoolManagementSystemApi.Model
{
    public class Students : BaseClass
    {
        public Guid Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public Guid ClassRoomID { get; set; }
        public List<Subjects> Subjects { get; set; } = new List<Subjects>();
        public List<Parents> Parents { get; set; } = new List<Parents>();
    }
}
