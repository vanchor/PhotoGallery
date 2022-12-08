using PhotoGallery.Models.UserDto;
using PhotoGallery.Services;
using PhotoGallery.Tests.Mocks;
using System.Net;

namespace PhotoGallery.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public void GivenAnIdOfAnExistingUser_WhenGettingUserById_ThenUserReturns()
        {
            var userRepos = MockUserRepository.GetMock();
            var userService = new UserService(userRepos.Object);

            var result = userService.GetById("Test1");

            Assert.NotNull(result);
            Assert.IsAssignableFrom<Entities.User>(result);
        }

        [Fact]
        public void GivenAnIdOfANonExistingUser_WhenGettingUserById_ThenNull()
        {
            var userRepos = MockUserRepository.GetMock();
            var userService = new UserService(userRepos.Object);

            var result = userService.GetById("NonExistingUser");

            Assert.Null(result);
        }

        [Fact]
        public async void GivenValidRequest_WhenRegisterUser_ThenTokenReturns()
        {
            var userRepos = MockUserRepository.GetMock();
            var userService = new UserService(userRepos.Object);

            var user = new UserModel()
            {
                Username = "NewNonExistingUser",
                Email = "qwer@gmail.com",
                Password = "1234",
            };

            var result = await userService.Register(user);

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.OK, result!.StatusCode);
            Assert.Equal(user.Username, result!.Data.Username);
        }

        [Fact]
        public async void GivenExistingUser_WhenRegisterUser_ThenConflict()
        {
            var userRepos = MockUserRepository.GetMock();
            var userService = new UserService(userRepos.Object);

            var user = new UserModel()
            {
                Username = "Test1",
                Email = "qwer@gmail.com",
                Password = "1234",
            };

            var result = await userService.Register(user);

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.Conflict, result!.StatusCode);
        }

        [Theory]
        [InlineData("Test1", "IncorrectPassword")]
        [InlineData("IncorrectName", "")]
        public async void GivenInvlidUser_WhenAuthenticaterUser_ThenTokenReturns(string Username, string Password)
        {
            var userRepos = MockUserRepository.GetMock();
            var userService = new UserService(userRepos.Object);

            var user = new LoginViewModel()
            {
                Username = Username,
                Password = Password,
            };

            var result = await userService.Authenticate(user);

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.UnprocessableEntity, result!.StatusCode);
        }
    }
}