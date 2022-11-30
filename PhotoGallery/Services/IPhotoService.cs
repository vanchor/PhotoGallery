using PhotoGallery.Entities;
using PhotoGallery.Models;

namespace PhotoGallery.Services
{
    public interface IPhotoService
    {
        Task CreatePhoto(PhotoVM photoVM);
        BaseResponse<IEnumerable<Photo>> GetAll(string? Username = null);
    }
}
