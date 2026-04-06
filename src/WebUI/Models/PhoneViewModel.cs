using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class PhoneViewModel
    {
        public int Id { get; set; }

        public int ContactId { get; set; }

        [Required(ErrorMessage = "The Phone Number field has to be filled.")]
        [MinLength(9, ErrorMessage = "The Phone Number field must have at least 9 characters.")]
        [MaxLength(100, ErrorMessage = "The Phone Number field must have at most 100 characters.")]
        [DisplayName("Phone Number")]
        public string? PhoneNumber { get; set; }
    }
}
