namespace SchoolManagementSystemApi.DTOModel
{
    public class TimeTableDTO
    {
        public Guid ClassId { get; set; }
        public Guid SubjectId { get; set; }
        public DateTime TimeSchedule { get; set; }
    }
}
