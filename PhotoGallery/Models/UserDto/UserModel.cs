using System.ComponentModel.DataAnnotations;

namespace PhotoGallery.Models.UserDto
{
    public class UserModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Username cannot have special character or spaces")]
        [MaxLength(10)]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        [RegularExpression(@"^\S*$", ErrorMessage = "Email cannot have spaces")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
