using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EventManager.Dto
{

    [Obsolete]
    public class EventMeal
    {
        [Key]
        public Guid UniqueId { get; set; }
        public Guid? EventId { get; set; }
        [ForeignKey("EventId")]
        public Event? Event { get; set; } = null;

        public string MealName { get; set; } = string.Empty;
        [DisplayName("Start Date Time")]
        public DateTime? StartDateTime { get; set; }
        [DisplayName("Start Date Time")]
        public DateTime? EndDateTime { get; set; }

    }
}

