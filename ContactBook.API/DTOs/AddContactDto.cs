using System.ComponentModel.DataAnnotations;

namespace ContactBook.DTO
{
    public class AddContactDto
    {
        [Required]
        [StringLength (50, MinimumLength = 8, ErrorMessage = "name cannot be less than eight alphabets")]
        public string FirstName { get; set; } = string.Empty;




        [Required]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "name cannot be less than eight alphabets")]
        public string LastName { get; set; } = string.Empty;



        [Required]
        [StringLength (15, MinimumLength = 11, ErrorMessage = " Number must  not exceed eleven digits")]
        public string PhoneNumber { get; set; } = string.Empty;



        [Required]
        [EmailAddress (ErrorMessage = "invalid email format")]
        public string Email { get; set; } = string.Empty;


        [Required]
        public string Address { get; set; } = string.Empty;

    }
}
