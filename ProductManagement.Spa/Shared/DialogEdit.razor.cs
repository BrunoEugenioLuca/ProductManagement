using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProductManagement.Models;
using System.Net.Http.Json;

namespace ProductManagement.Spa.Shared
{
    public partial class DialogEdit : ComponentBase
    {
        [CascadingParameter] 
        MudDialogInstance MudDialog { get; set; }

        [Parameter] 
        public ProductDto? Product { get; set; }

        [Inject]
        private HttpClient? _HttpClient { get; set; }

        private void Cancel() => MudDialog.Cancel();

        private async void UpdateProduct()
        {
            if(_HttpClient != null)
            {
                if(Product != null)
                {
                    var response = await _HttpClient.PutAsJsonAsync($"Product/UpdateProduct/{Product.Id}", Product);
                    Console.WriteLine(response);
                    Console.WriteLine(Product.ProductName);
                    Console.WriteLine(Product.ShortDescription);
                    Console.WriteLine(Product.Price);


                }

            }
          
            Snackbar.Add("Product Updated", Severity.Success);
            MudDialog.Close(DialogResult.Ok(true));
        }
    
    }
}
