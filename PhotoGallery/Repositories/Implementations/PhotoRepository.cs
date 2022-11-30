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

        public IEnumerable<Photo> GetAllByUserWithPagination(string username, int count = 0, int page = 0)
        {
            return _context.Photos.Where(p => p.Username == username).OrderByDescending(p => p.Date).Skip(page * count).Take(count).ToList();
        }
    }
}
