using Microsoft.AspNetCore.Components;
using PhotoGallery.Models.UserDto;
using PhotoGallery.Infrastructure;
using System.ComponentModel.DataAnnotations;
using PhotoGallery.Services;
using System.Net;
using Microsoft.AspNetCore.Components.Forms;

namespace PhotoGallery.Pages
{
    public class LoginModel : ComponentBase
    {
        [Inject] public ILocalStorageService localStorageService { get; set; }
        [Inject] public NavigationManager navigationManager { get; set; }
        [Inject] private IUserService _userService { get; set; }

        public LoginModel()
        {
            LoginData = new LoginViewModel();
            UserModel = new UserModel();
        }

        public LoginViewModel LoginData { get; set; }
        public string LoginError { get; set; }

        protected async Task LoginAsync()
        {
            var response = await _userService.Authenticate(LoginData);
            if(response.StatusCode == HttpStatusCode.OK)
            {
                await localStorageService.SetAsync(nameof(SecurityToken), response.Data);

                navigationManager.NavigateTo("/", true);
            }
            else
            {
                LoginError = response.Description; 
                StateHasChanged();
            }
        }

        public UserModel UserModel { get; set; }
        public string RegisterError { get; set; }

        protected async Task RegisterAsync()
        {
            var response = await _userService.Register(UserModel);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                await localStorageService.SetAsync(nameof(SecurityToken), response.Data);

                navigationManager.NavigateTo("/", true);
            }
            else
            {
                RegisterError = response.Description;
                StateHasChanged();
            }
        }
    }
}
