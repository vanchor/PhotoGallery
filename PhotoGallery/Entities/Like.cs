using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoGallery.Entities
{
    public class Like
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public string Username { get; set; }
        public int PhotoId { get; set; }

        public virtual Photo Photo { get; set; }
        public virtual User User { get; set; }
    }
}
