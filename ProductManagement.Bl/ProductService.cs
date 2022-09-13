using Microsoft.EntityFrameworkCore;
using ProductManagement.Dal.Data;
using ProductManagement.Dal.Entity;
using ProductManagement.Producer;

namespace ProductManagement.Bl
{
    public class ProductService : IProductService
    {
        private ProductManagementContext _context;
        private IProducerMessage _producer;

        public ProductService(ProductManagementContext context, IProducerMessage producer)
        {
            _context = context;
            _producer = producer;
        }

        public async Task InsertNewProductAsync(Product product)
        {
            await _context.AddAsync(product);
            _context.SaveChanges();

            await _producer.PushMessageAsync(product);
        }
        public async Task DeleteProductByIdAsync(int id)
        {
            try
            {
                if(!_context.Product.Any(x => x.Id == id))
                {
                    throw new ArgumentException($"Product {id} not found");
                }

                var del = await GetProductByIdAsync(id);
                _context.Product.Remove(del);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error: {ex.Message}");
            }
        }

        public async Task<List<Product>> GetAllProductsAsync()
            => await _context.Product.ToListAsync();
        

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _context.Product.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (product == null) throw new NotImplementedException();
            return product;
        }

        public async Task UpdateProductAsync(Product product)
        {
            await Task.Run(() =>
            {
                _context.Product.Update(product);
                _context.SaveChanges(true);
            });

            await _producer.PushMessageAsync(product);

        }
    }

    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<List<Product>> GetAllProductsAsync();
        Task InsertNewProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductByIdAsync(int id);

    }
}
