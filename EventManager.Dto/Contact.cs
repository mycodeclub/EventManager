using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EventManager.Dto
{ 
    public class Contact
    {
        [Key]
        public Guid UniqueId { get; set; }
        [Required(ErrorMessage = "Person Name can't be empty")]
        public string FullName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; } = string.Empty;


        [DisplayName("Primary Phone No.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        //[Required(ErrorMessage = "Please Input a valid Mobile No.")]
        public string? PrimaryMobileNo { get; set; } = string.Empty;


        [DisplayName("Secondry Phone No.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string? SecondryMobileNo { get; set; } = string.Empty;

    }
}
