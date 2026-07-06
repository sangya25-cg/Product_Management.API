using Microsoft.EntityFrameworkCore;
using ProductManagement.API.Data;
using ProductManagement.API.Models;

namespace ProductManagement.API.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductService> _logger;

        public ProductService(ApplicationDbContext context, ILogger<ProductService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            _logger.LogInformation("Retrieving all products");
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            _logger.LogInformation("Retrieving product with ID: {ProductId}", id);
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            product.CreatedDate = DateTime.UtcNow;
            product.UpdatedDate = null;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Product created with ID: {ProductId}, Name: {ProductName}", product.Id, product.Name);
            return product;
        }

        public async Task<Product?> UpdateProductAsync(int id, Product product)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
            {
                _logger.LogWarning("Product with ID: {ProductId} not found for update", id);
                return null;
            }

            existingProduct.Name = product.Name;
            existingProduct.Category = product.Category;
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;
            existingProduct.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Product updated with ID: {ProductId}, Name: {ProductName}", existingProduct.Id, existingProduct.Name);
            return existingProduct;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                _logger.LogWarning("Product with ID: {ProductId} not found for deletion", id);
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Product deleted with ID: {ProductId}, Name: {ProductName}", product.Id, product.Name);
            return true;
        }

        public async Task<IEnumerable<Product>> SearchProductsByCategoryAsync(string category)
        {
            _logger.LogInformation("Searching products by category: {Category}", category);
            return await _context.Products
                .Where(p => p.Category.ToLower() == category.ToLower())
                .ToListAsync();
        }
    }
}
