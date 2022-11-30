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

        public BaseResponse<IEnumerable<Photo>> GetAll(string? Username)
        {
            IEnumerable<Photo> photos;
            if (Username == null)
                photos = _photoRepository.GetAll();
            else
                photos = _photoRepository.GetAllByUser(Username);

            return new BaseResponse<IEnumerable<Photo>>() {
              Data = photos.OrderByDescending(g => g.Date),
              StatusCode = System.Net.HttpStatusCode.OK
            };
        }
    }
}
