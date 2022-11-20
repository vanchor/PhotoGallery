using PhotoGallery.Data;
using PhotoGallery.Entities;
using PhotoGallery.Repositories.Interfaces;

namespace PhotoGallery.Repositories.Implementations
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(PhotoGalleryDbContext context) : base(context)
        {
        }

        public User? GetById(string Username)
        {
            return _context.Users.Find(Username);
        }

        public async Task<User?> GetByIdAsync(string Username)
        {
            return await _context.Users.FindAsync(Username);
        }
    }
}
