using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EventManager.Dto
{
    public class GuestEventAttendance
    {
        [Key]
        public Guid UniqueId { get; set; }

        public Guid? EventId { get; set; }

        public Guid? GuestId { get; set; }

        [DisplayName("Check-In Time")]
        public DateTime? CheckIn { get; set; }

        [DisplayName("Check-Out Time")]
        public DateTime? CheckOut { get; set; }

    }
}
