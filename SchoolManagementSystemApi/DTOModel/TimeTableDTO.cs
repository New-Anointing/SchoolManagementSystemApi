namespace SchoolManagementSystemApi.DTOModel
{
    public class TimeTableDTO
    {
        public Guid ClassId { get; set; }
        public Guid SubjectId { get; set; }
        public DateTime StartTime{ get; set; }
        public DateTime EndTime { get; set; }
    }
}
