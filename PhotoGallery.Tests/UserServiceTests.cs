using PhotoGallery.Models.UserDto;
using PhotoGallery.Services;
using PhotoGallery.Tests.Mocks;

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
        public void GiveNewUser_WhenCreating()
        {
            var userRepos = MockUserRepository.GetMock();
            var userService = new UserService(userRepos.Object);

            var user = new UserModel()
            {
                Username = "NewNonExistingUser",
                Email = "qwer@gmail.com",
                Password = "1234",
            };

            var result = userService.Register(user);

            Assert.NotNull(result);
        }
    }
}