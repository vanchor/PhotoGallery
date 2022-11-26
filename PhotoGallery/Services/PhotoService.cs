using PhotoGallery.Entities;
using PhotoGallery.Helpers;
using PhotoGallery.Models;
using PhotoGallery.Repositories.Interfaces;

namespace PhotoGallery.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoRepository _photoRepository;

        public PhotoService(IPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }

        public async Task CreatePhoto(PhotoVM photoVM)
        {
            var fullPathToUserFolderInMedia = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "media", photoVM.Username);
            FileHelper.CreateDirectory(fullPathToUserFolderInMedia);

            var photoName = Guid.NewGuid().ToString() + ".jpg";

            var photo = new Photo()
            {
                FileName = photoName,
                Title = photoVM.Title,
                Date = DateTime.Now,
                Username = photoVM.Username,
            };

            await FileHelper.UploadFileAsync(photoVM.file, Path.Combine(fullPathToUserFolderInMedia, photoName));

            _photoRepository.Add(photo);
            await _photoRepository.SaveChangesAsync();
        }

        public BaseResponse<IEnumerable<Photo>> GetAll()
        {
            return new BaseResponse<IEnumerable<Photo>>() {
              Data = _photoRepository.GetAll().OrderByDescending(g => g.Date),
              StatusCode = System.Net.HttpStatusCode.OK
            };
        }
    }
}
