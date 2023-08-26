using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Dto
{
    public class Event
    {
        [Key]
        public Guid UniqueId { get; set; }

        public Guid? ParentEventId { get; set; }

        [ForeignKey("ParentEventId")]
        public Event? ParentEvent { get; set; } = null;

        public Guid? EventOrganizerId { get; set; }

        [ForeignKey("EventOrganizerId")]
        public EventPlannerOrg? Organizer { get; set; }

        [DisplayName("Event Title")]
        public string Title { get; set; } = string.Empty;
        [DisplayName("Event Description")]
        public string Description { get; set; } = string.Empty;
        [DisplayName("Venue (Address)")]
        public string VenueAddress { get; set; } = string.Empty;
        [DisplayName("Start Date Time")]
        public DateTime? StartDate { get; set; }
        [DisplayName("End Date Time")]
        public DateTime? EndDate { get; set; }


        [NotMapped]
        public List<Guest>? Guests { get; set; }
        public string Duration
        {
            get
            {
                if (EndDate.HasValue && StartDate.HasValue)
                {
                    var dateDiff = (EndDate.Value - StartDate.Value);
                    if (dateDiff.TotalSeconds > 0)
                    {
                        var diff = new EventDuration() { days = dateDiff.Days, hours = dateDiff.Hours, minutes = dateDiff.Minutes };
                        StringBuilder sb = new StringBuilder();
                        if (diff.days > 0) sb.Append(diff.days + " d");
                        if (diff.hours > 0)
                        {
                            if (diff.days > 0)
                                sb.Append(" | ");
                            sb.Append(diff.hours + " h");
                        }
                        if (diff.minutes > 0) sb.Append(" | " + diff.minutes + " m");

                        return sb.ToString();
                    }
                }
                return string.Empty;
            }
        }


        [DisplayName("Days To Go")]
        [NotMapped]
        public int DaysToGo
        {
            get
            {
                if (StartDate != null)
                {
                    var diffOfDates = (StartDate.Value - DateTime.Now);
                    return diffOfDates.Days > 0 ? diffOfDates.Days : 0;
                }
                else return 0;
            }
        }

    }

    public enum EventStatus { Scheduled, InProgress, Done }

    public class EventDuration
    {
        public int days { get; set; }
        public int hours { get; set; }
        public int minutes { get; set; }
    }
}
