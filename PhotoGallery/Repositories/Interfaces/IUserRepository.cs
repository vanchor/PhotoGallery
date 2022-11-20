using PhotoGallery.Entities;

namespace PhotoGallery.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User? GetById(string Username);
        Task<User?> GetByIdAsync(string Username);
    }
}
