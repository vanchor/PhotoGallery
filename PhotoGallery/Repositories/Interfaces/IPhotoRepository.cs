using PhotoGallery.Entities;

namespace PhotoGallery.Repositories.Interfaces
{
    public interface IPhotoRepository : IBaseRepository<Photo>
    {
        IEnumerable<Photo> GetAllByUser(string username);
    }
}
