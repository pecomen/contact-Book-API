using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ContactBook.DTOs
{
    public class AddUserDto
    {
        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "only 3 -15 characters allowed!")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "only 3 -15 characters allowed!")]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string password { get; set; }

        public string PhoneNumber { get; set; }
    }
}
