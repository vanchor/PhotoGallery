using PhotoGallery.Entities;
using PhotoGallery.Helpers;
using PhotoGallery.Models;
using PhotoGallery.Models.UserDto;
using PhotoGallery.Repositories.Interfaces;

namespace PhotoGallery.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<SecurityToken>> Authenticate(LoginViewModel model)
        {
            var userInDb = await _userRepository.GetByIdAsync(model.Username);

            if (userInDb == null ||
                !HashPasswordHelper.VerifyPasswordHash(model.Password, userInDb.PasswordHash, userInDb.PasswordSalt))
            {
                return new BaseResponse<SecurityToken>(){
                    StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
                    Description = "Username or password is incorrect."
                };
            }

            var token = new SecurityToken()
            {
                Username = model.Username,
                AccessToken = model.Password,
                ExpiredAt = DateTime.UtcNow.AddMinutes(5)
            };

            return new BaseResponse<SecurityToken>() {
                StatusCode = System.Net.HttpStatusCode.OK,
                Data = token
            };
        }

        public User GetById(string id)
        {
            return _userRepository.GetById(id);
        }

        public async Task<BaseResponse<SecurityToken>> Register(UserModel userModel)
        {
            
            if(_userRepository.GetById(userModel.Username) != null)
                return new BaseResponse<SecurityToken>(){
                    StatusCode = System.Net.HttpStatusCode.Conflict,
                    Description = "User already exists."
                };

            HashPasswordHelper.CreatePasswordHash(userModel.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new User()
            {
                Username = userModel.Username,
                Email = userModel.Email,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            var response = await Authenticate(new LoginViewModel
            {
                Username = userModel.Username,
                Password = userModel.Password
            });

            return response;
        }
    }
}
