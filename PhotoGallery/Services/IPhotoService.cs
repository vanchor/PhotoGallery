using PhotoGallery.Entities;
using PhotoGallery.Models;

namespace PhotoGallery.Services
{
    public interface IPhotoService
    {
        Task CreatePhoto(PhotoVM photoVM);
        BaseResponse<PhotosPaginationVM> GetAll(string? Username = null, int postsPerPage = 5, int page = 0);
    }
}
