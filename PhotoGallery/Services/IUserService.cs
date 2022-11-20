using PhotoGallery.Entities;
using PhotoGallery.Models;
using PhotoGallery.Models.UserDto;

namespace PhotoGallery.Services
{
    public interface IUserService
    {
        Task<BaseResponse<SecurityToken>> Register(UserModel userModel);
        Task<BaseResponse<SecurityToken>> Authenticate(LoginViewModel model);

        User GetById(string id);
    }
}
