using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class PhoneViewModel
    {
        public int Id { get; set; }

        public int ContactId { get; set; }

        [Required(ErrorMessage = "The Phone Number field has to be filled.")]
        [RegularExpression(@"^(\(\d{2}\)\d{4,5}-\d{4}|\d{10,11})$", ErrorMessage = "Use a valid format: (xx)xxxx-xxxx or (xx)xxxxx-xxxx.")]
        [DisplayName("Phone Number")]
        public string? PhoneNumber { get; set; }
    }
}
