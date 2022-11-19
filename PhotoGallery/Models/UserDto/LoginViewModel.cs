using System.ComponentModel.DataAnnotations;

namespace PhotoGallery.Models.UserDto
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
