using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MudBlazor;
using ProductManagement.Models;
using System.Net.Http.Json;

namespace ProductManagement.Spa.Pages
{
    
    public partial class Login : ComponentBase
    {
        public UsersDto? User { get; set; } = new UsersDto();
      

        [Inject]
        private HttpClient? Client { get; set; }

        public async Task SingIn()
        {
            if (User != null)
            {
                var response = await Client.PostAsJsonAsync("Users/CheckIn", User);
                if (response.IsSuccessStatusCode)
                {
                    Snackbar.Add($"Benvenuto {User.Username}", Severity.Success);
                    Navigation.NavigateTo($"products/{User.Username}");
                }
                else
                {
                    Snackbar.Add("Username o Password errati", Severity.Error);
                    User = new UsersDto();
                    StateHasChanged();
                }
            }
        }
    }
}
