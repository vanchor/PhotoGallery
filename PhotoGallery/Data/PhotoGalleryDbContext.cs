using Microsoft.EntityFrameworkCore;
using PhotoGallery.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace PhotoGallery.Data
{
    public class PhotoGalleryDbContext : DbContext
    {

        public DbSet<Category> Categories { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }

        public PhotoGalleryDbContext(DbContextOptions<PhotoGalleryDbContext> options) : base(options)
        {
            try
            {
                var dbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (dbCreator != null)
                {
                    if (!dbCreator.CanConnect()) dbCreator.Create();
                    if (!dbCreator.HasTables()) dbCreator.CreateTables();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity("CategoryPhoto", b =>
            {
                b.HasOne("PhotoGallery.Entities.Category", null)
                    .WithMany()
                    .HasForeignKey("CategoriesId")
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired(false);

                b.HasOne("PhotoGallery.Entities.Photo", null)
                    .WithMany()
                    .HasForeignKey("PhotosId")
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired(false);
            });

            builder.Entity<Like>(b =>
            {
                b.HasOne(x => x.Photo)
                    .WithMany(x => x.Likes)
                    .HasForeignKey(x => x.PhotoId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();

                b.HasOne(x => x.User)
                    .WithMany(x => x.Likes)
                    .HasForeignKey(x => x.Username)
                    .OnDelete(DeleteBehavior.NoAction)
                    .IsRequired();
            });

            base.OnModelCreating(builder);
        }
    }
}
