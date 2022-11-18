using Microsoft.AspNetCore.Components;
using PhotoGallery.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace PhotoGallery.Pages
{
    public class LoginModel : ComponentBase
    {
        [Inject] public ILocalStorageService localStorageService { get; set; }
        [Inject] public NavigationManager navigationManager { get; set; }
        public LoginModel()
        {
            LoginData = new LoginViewModel();
        }

        public LoginViewModel LoginData { get; set; }

        protected async Task LoginAsync()
        {
            var token = new SecurityToken()
            {
                Username = LoginData.Username,
                AccessToken = LoginData.Password,
                ExpiredAt = DateTime.UtcNow.AddMinutes(5)
            };
            await localStorageService.SetAsync(nameof(SecurityToken), token);

            navigationManager.NavigateTo("/");
        }
    }

    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
    public class SecurityToken
    {
        public string Username { get; set; }

        public string AccessToken { get; set; }

        public DateTime ExpiredAt { get; set; }
    }
}
