using PhotoGallery.Entities;

namespace PhotoGallery.Models
{
    public class PhotosPaginationVM
    {
        public IEnumerable<Photo> Photos { get; set; }
        public int PageNumber { get; set; }
        public int NumberOfPages { get; set; }
    }
}
