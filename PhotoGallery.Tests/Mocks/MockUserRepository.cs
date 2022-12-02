using Moq;
using PhotoGallery.Entities;
using PhotoGallery.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery.Tests.Mocks
{
    internal class MockUserRepository
    {
        public static Mock<IUserRepository> GetMock()
        {
            var mock = new Mock<IUserRepository>();

            var Users = new List<User>()
            {
                new User()
                {
                   Username = "Test1",
                   Email="test@gmail.com",
                   PasswordHash=new byte[32],
                   PasswordSalt=new byte[32]
                },
                new User()
                {
                   Username = "Test2",
                   Email="test2@gmail.com",
                   PasswordHash=new byte[32],
                   PasswordSalt=new byte[32]
                }
            };
            mock.Setup(m => m.GetAll()).Returns(() => Users);
            mock.Setup(m => m.GetById(It.IsAny<String>()))
                .Returns((string username) => Users.FirstOrDefault(u => u.Username == username));

            mock.Setup(m => m.Add(It.IsAny<User>())).Callback(() => { return; });
            mock.Setup(m => m.AddRange(It.IsAny<IEnumerable<User>>())).Callback(() => { return; });
            mock.Setup(m => m.Remove(It.IsAny<User>())).Callback(() => { return; });
            mock.Setup(m => m.RemoveRange(It.IsAny<IEnumerable<User>>())).Callback(() => { return; });
            mock.Setup(m => m.SaveChanges()).Callback(() => { return; });
            return mock;
        }
    }
}
