using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystemApi.Model
{
    public class Teachers
    {
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public Guid ClassRoomId { get; set; }
        [ForeignKey("ClassRoomId")]
        public virtual ClassRoom ClassRoom { get; set; }


    }
}
