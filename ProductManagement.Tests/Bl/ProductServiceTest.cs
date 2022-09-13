using NUnit.Framework;
using ProductManagement.Bl;
using ProductManagement.Dal.Data;
using ProductManagement.Dal.Entity;
using ProductManagement.Producer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Tests.Bl
{
    public class ProductServiceTest
    {
        private IProductService _productService;
        private IProducerMessage _producer;
        private ProductManagementContext _context;

        public ProductServiceTest()
        {
            _context = new ProductManagementContext();
            _producer = new ProducerMessage();
            _productService = new ProductService(_context,_producer);
            
        }

        [Test]
        public void Get_Product_By_Id()
        {
            var product = _productService.GetProductByIdAsync(1).Result;
            
            Assert.IsNotNull(product);
            Assert.IsTrue(product.Price == 700);
            Assert.IsTrue(product.ShortDescription == "ottimo telefono");
            Assert.IsTrue(product.ProductName == "oppo find x5");
        }

        [Test]
        public void Insert_New_Product()
        {
            var product = new Product()
            {
                Price = 250,
                ShortDescription = "ciao come stai",
                ProductName = "Saluto",
                FullDescription = "odaopjdoisajdoiajdèoisajd"
               
            };

            _productService.InsertNewProductAsync(product);
            Assert.IsTrue(_context.Product.Any(x => x.Id == product.Id));
        }

        [Test]
        public async Task Update_Product_By_Specific_Id()
        {
            var oldProduct = 3;
            var updateProduct = _productService.GetProductByIdAsync(oldProduct).Result;
            updateProduct.ProductName = "Saluto 2.0";
            await _productService.UpdateProductAsync(updateProduct);

            Assert.IsTrue(_context.Product.Any(x => x.Id == oldProduct && x.ProductName == updateProduct.ProductName));
        }

        [Test]
        public async Task Delete_Product_By_Specific_Id()
        {
            var oldId = 2;
            var product = _productService.GetProductByIdAsync(oldId).Result;
            if(product != null)
            {
                await _productService.DeleteProductByIdAsync(oldId);
                Assert.IsTrue(!_context.Product.Any(x => x.Id == oldId));
            }
            else
            {
                Assert.IsTrue(product == null);
            }
        }
    }
}
