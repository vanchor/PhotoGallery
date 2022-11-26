using Microsoft.AspNetCore.Components;
using PhotoGallery.Infrastructure;
using System.ComponentModel.DataAnnotations;
using PhotoGallery.Services;
using System.Net;
using Microsoft.AspNetCore.Components.Forms;
using PhotoGallery.Entities;

namespace PhotoGallery.Pages
{
    public class IndexModel : ComponentBase
    {
        [Inject] public NavigationManager navigationManager { get; set; }
        [Inject] protected IPhotoService _photoService { get; set; }
        public IEnumerable<Photo>? photos { get; set; } = null;

        public IndexModel()
        {
            
        }

        protected override void OnInitialized()
        {
            photos = _photoService.GetAll().Data;
        }
    }
}
