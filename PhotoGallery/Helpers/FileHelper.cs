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

        public static async Task<string> GeneratePhotoPreviewAsync(IBrowserFile file, int width = 100, int height = 100)
        {
            var resizedImage = await file.RequestImageFileAsync(file.ContentType, width, height);
            var buffer = new byte[resizedImage.Size];
            await resizedImage.OpenReadStream().ReadAsync(buffer);
            return $"data:{file.ContentType};base64,{Convert.ToBase64String(buffer)}";
        }

        public static async Task ResizeImageAsync(IBrowserFile file, string imagePath, int newWidth = 0, int newHeight = 0)
        {
            if (!file.ContentType.Contains("image"))
                throw new ArgumentException("The file is not an image!");

            await using MemoryStream ms = new MemoryStream();
            await file.OpenReadStream(maxAllowedSize: 1000000).CopyToAsync(ms);

            Image image = Image.FromStream(ms, true, true);
            if (newHeight == 0 && newWidth > 0)
            {
                var multiplier = (float)newWidth / image.Width;
                newHeight = (int)(multiplier * image.Height);
            }
            else if (newWidth == 0 && newHeight > 0)
            {
                var multiplier = (float)newHeight / image.Height;
                newWidth = (int)(multiplier * image.Width);
            }
            else
                throw new ArgumentException("The height and width cannot be less than 0. At least one of the parameters must be greater than 0.");

            Bitmap resized = new Bitmap(image, newWidth, newHeight);
            resized.Save(imagePath);
        }
    }
}