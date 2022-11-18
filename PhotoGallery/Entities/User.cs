using System.ComponentModel.DataAnnotations;

namespace PhotoGallery.Entities
{
    public class User
    {
        [Key]
        public string Username { get; set; }

        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
