using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoGallery.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string? Title { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("User")]
        public string Username { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Category>? Categories { get; set; }
        public virtual ICollection<Like>? Likes { get; set; }
    }
}
