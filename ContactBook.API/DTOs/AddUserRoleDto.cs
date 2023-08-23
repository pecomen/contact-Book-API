using System.ComponentModel.DataAnnotations;

namespace ContactBook.DTOs
{

    public class AddUserRoleDto
    {
        [Required]
        public string UserId { get; set; } = "";


        [Required]
        public string Role { get; set; } = "";
        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public string PhoneNumber { get; internal set; }
        public string Email { get; internal set; }
        public string Password { get; internal set; }
    }
}
