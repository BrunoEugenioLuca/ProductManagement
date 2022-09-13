using Microsoft.AspNetCore.Mvc;
using ProductManagement.Bl;
using ProductManagement.Dal.Entity;
using ProductManagement.Models;

namespace ProductManagement.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetProductById/{productId}")]
        public async Task<IActionResult> GetProduct(int productId)
        => Ok(await _service.GetProductByIdAsync(productId));

        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var expectedResult = await _service.GetAllProductsAsync();
            var resultForClient = expectedResult.Select(x => new ProductDto()
            {
                Id = x.Id,
                Price = x.Price,
                ShortDescription = x.ShortDescription,
                FullDescription = x.FullDescription,
                ProductName = x.ProductName,
                Published = x.Published
            });
            
            return Ok(resultForClient);
        }
           

        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct(ProductDto productDto)
        {
            if(productDto == null) throw new ArgumentNullException("Parametro non valido");

            var product = new Product()
            {
                ProductName = productDto.ProductName,
                FullDescription = productDto.FullDescription,
                ShortDescription = productDto.ShortDescription,
                Price = productDto.Price,
                Published = productDto.Published
            };

            await _service.InsertNewProductAsync(product);

            return Ok(product);
        }
        
        [HttpPut]
        [Route("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(ProductDto productDto, int id)
        {
            var product = await _service.GetProductByIdAsync(id);
            if (product == null) throw new ArgumentException($"Product {id} not found");

            product.FullDescription = productDto.FullDescription;
            product.ShortDescription = productDto.ShortDescription;
            product.Price = productDto.Price;
            product.ProductName = productDto.ProductName;
            product.Published = productDto.Published;

            await _service.UpdateProductAsync(product);

            return Ok(product);

        }

        [HttpDelete]
        [Route("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _service.DeleteProductByIdAsync(id);
            return NoContent();
        }
            
    }
}
