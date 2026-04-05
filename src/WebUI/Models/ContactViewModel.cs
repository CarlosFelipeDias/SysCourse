using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class ContactViewModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "The Name field has to be filled.")]
        [MinLength(3, ErrorMessage = "The Name field must have at least 3 characters.")]
        [MaxLength(100, ErrorMessage = "The Name field must have at most 100 characters.")]
        [DisplayName("First Name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "The Surname field has to be filled.") ]
        [MinLength(2, ErrorMessage = "The Surname field must have at least 2 characters.")]
        [MaxLength(100, ErrorMessage = "The Surname field must have at most 100 characters.")]
        [DisplayName("Surname")]
        public string? Surname { get; set; }
        
        [Required(ErrorMessage = "The Email field has to be filled.")]
        [MinLength(10, ErrorMessage = "The Email field must have at least 10 characters.")]
        [MaxLength(100, ErrorMessage = "The Email field must have at most 100 characters.")]
        [EmailAddress(ErrorMessage = "The Email field must be a valid email address.")]
        [DisplayName("Email")]
        public string? Email { get; set; }
    }
}
