using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MudBlazor;
using ProductManagement.Dal.Entity;
using ProductManagement.Models;
using ProductManagement.Spa.Shared;
using System.Net.Http.Json;
using System.Text.Json;
using static MudBlazor.DialogService;

namespace ProductManagement.Spa.Pages
{
    public partial class Products : ComponentBase
    {
        [Parameter]
        public string? Username { get; set; }
        public List<ProductDto>? ListProducts { get; set; } = new List<ProductDto>();
        
        public ProductDto P { get; set; }
        [Inject]
        private HttpClient HttpClient { get; set; }

        protected override async Task OnInitializedAsync()
        {

            await LoadTableDataAsync();
            _ = base.OnInitializedAsync();
        }

        private async Task LoadTableDataAsync()
        {
            ListProducts = await HttpClient.GetFromJsonAsync<List<ProductDto>>("Product/GetAllProducts");
        }
        
        public async Task Publishproduct(ProductDto product)
        {
            product.Published = !product.Published;
            Console.WriteLine(product);
            Console.WriteLine(product);
            var result = await HttpClient.PutAsJsonAsync($"Product/UpdateProduct/{product.Id}", product);
            Console.WriteLine(result);
            Snackbar.Add("Product Updated", Severity.Success);
        }

        public async Task OpenDialogEdit(ProductDto product)
        {
            var parameters = new DialogParameters { ["product"] = product };
            var tmp = DialogService.Show<DialogEdit>("Update Product", parameters);
            var res = await tmp.Result;

            if (res.Data == null)
            {
                await LoadTableDataAsync();
                StateHasChanged();
            }
        }

        public void AddProduct()
        {
            Navigation.NavigateTo($"/formProduct/{Username}");
        }
        //public async Task OpenDialogDelete(ProductDto productId)
        //{
        //    var parameters = new DialogParameters { ["Product"] = productId };
        //    var options = new DialogOptions { Position = DialogPosition.TopCenter };
        //    var tmp = DialogService.Show<DialogDelete>("Delete Product", parameters,options);

        //    var res = await tmp.Result;

        //    if (res.Data == null)
        //    {
        //        await LoadTableDataAsync();
        //        StateHasChanged();
        //    }
        //}
    }
}
