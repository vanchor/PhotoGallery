namespace PhotoGallery.Models.UserDto
{

    public class SecurityToken
    {
        public string Username { get; set; }

        public string AccessToken { get; set; }

        public DateTime ExpiredAt { get; set; }
    }
}
