using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProductManagement.Models;

namespace ProductManagement.Spa.Shared
{
    public partial class DialogDelete : ComponentBase
    {
        
        [CascadingParameter] 
        MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public ProductDto? Product { get; set; }


        [Inject]
        private HttpClient? _HttpClient { get; set; }


        private void Exit() => MudDialog.Cancel();

        private async void DeleteProduct()
        {
            if (_HttpClient != null)
            {
                if (Product != null)
                {
                    var response = await _HttpClient.DeleteAsync($"Product/DeleteProduct/{Product.Id}");
                    Console.WriteLine(response);
                   
                }

            }
          
            Snackbar.Add("Product Deleted", Severity.Success);
            MudDialog.Close();
        }
    }
}
