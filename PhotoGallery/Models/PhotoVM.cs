using Microsoft.AspNetCore.Components.Forms;

namespace PhotoGallery.Models
{
    public class PhotoVM
    {
        public IBrowserFile file { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
    }
}
