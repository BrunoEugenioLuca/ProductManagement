using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MudBlazor;
using ProductManagement.Models;
using ProductManagement.Spa.Shared;

using System.Net.Http.Json;

namespace ProductManagement.Spa.Pages
{
    public partial class FormAddProduct : ComponentBase
    {
        public ProductDto? Product { get; set; }
        [Parameter]
        public string? Username { get; set; }

        [Inject]
        private HttpClient? _HttpClient { get; set; }

        public bool FormValid
        {
            get
            {
                return !string.IsNullOrEmpty(Product.ProductName)
                                && !string.IsNullOrEmpty(Product.FullDescription)
                                && !string.IsNullOrEmpty(Product.ShortDescription)
                                && (Product.Price > 0);
            }
        }

        protected override Task OnInitializedAsync()
        {
            Product = new ProductDto();
            
            return base.OnInitializedAsync();
        }

        public async Task SaveProduct()
        {
            if(_HttpClient != null)
            { 
                //Product.Price = decimal.Round(Product.Price, 2);
                await _HttpClient.PostAsJsonAsync("Product/CreateProduct",Product);
                //Console.WriteLine($"{Product.ProductName} {Product.FullDescription} {Product.ShortDescription} {Product.Price}");
                OpenDialogSuccess();
                Product = new ProductDto();
                StateHasChanged(); 
                               
            }
        }
       
        public void OpenDialogSuccess()
        {
            var options = new DialogOptions { Position = DialogPosition.TopCenter, CloseButton = false };
            DialogService.Show<DialogSuccess>("Insert Product", options);
        }

        public void Exit()
        {
            Navigation.NavigateTo($"products/{Username}");
        }
      
    }
}
