using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManager.Dto
{
    public class EventPlannerOrg
    {
        [Key]
        public Guid UniqueId { get; set; }
        public Guid AppUserId { get; set; }
        [Required(ErrorMessage = "Event Organization Name Required")]
        public string OrgName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public Guid? PointOfContactId { get; set; }
        [ForeignKey("PointOfContactId")]
        public Contact? PointOfContact { get; set; }

        public string? WebsiteUrl { get; set; } = string.Empty;
        public string? LogoUri { get; set; } = string.Empty;
        public string? BackGroundUri { get; set; } = string.Empty;


        [DisplayName("Primary Phone No.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        //[Required(ErrorMessage = "Please Input a valid Mobile No.")]
        public string? PrimaryMobileNo { get; set; } = string.Empty;


        [DisplayName("Secondry Phone No.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string? SecondryMobileNo { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}