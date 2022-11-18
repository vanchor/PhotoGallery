using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoGallery.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("User")]
        public string Username { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
