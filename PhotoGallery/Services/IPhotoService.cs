using PhotoGallery.Models;

namespace PhotoGallery.Services
{
    public interface IPhotoService
    {
        Task CreatePhoto(PhotoVM photoVM);

    }
}
