using PhotoGallery.Entities;

namespace PhotoGallery.Repositories.Interfaces
{
    public interface IPhotoRepository : IBaseRepository<Photo>
    {
        IEnumerable<Photo> GetAllByUserWithPagination(string username, int count = 0, int page = 0);
    }
}
