using Microsoft.AspNetCore.Components.Forms;
using System.Drawing;

namespace PhotoGallery.Helpers
{
    public static class FileHelper
    {
        public static void CreateDirectory(string fullPath)
        {
            if (fullPath == null)
                throw new ArgumentNullException(nameof(fullPath));

            DirectoryInfo di;
            if (!Directory.Exists(fullPath))
                di = Directory.CreateDirectory(fullPath);
        }

        public static async Task UploadFileAsync(IBrowserFile file, string imagePath)
        {
            if (imagePath == null)
                throw new ArgumentNullException(nameof(imagePath));

            if (file is not null)
            {
                await using FileStream fs = new(imagePath, FileMode.Create);
                await file.OpenReadStream(maxAllowedSize: 1000000).CopyToAsync(fs);
            }
            else
                throw new ArgumentNullException(nameof(file));
        }

        public static void ResizeImage(IFormFile file, string imagePath, int newWidth, int newHeight = 0)
        {
            
        }

        public static async Task<string> GeneratePhotoPreviewAsync(IBrowserFile file, int width = 100, int height = 100)
        {
            var resizedImage = await file.RequestImageFileAsync(file.ContentType, width, height);
            var buffer = new byte[resizedImage.Size];
            await resizedImage.OpenReadStream().ReadAsync(buffer);
            return $"data:{file.ContentType};base64,{Convert.ToBase64String(buffer)}";
        }
    }
}