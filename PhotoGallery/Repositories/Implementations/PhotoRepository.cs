using PhotoGallery.Data;
using PhotoGallery.Entities;
using PhotoGallery.Repositories.Interfaces;

namespace PhotoGallery.Repositories.Implementations
{
    public class PhotoRepository : BaseRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(PhotoGalleryDbContext context) : base(context)
        {
        }

        public IEnumerable<Photo> GetAllByUser(string username)
        {
            return _context.Photos.Where(p => p.Username == username).ToList();
        }
    }
}
