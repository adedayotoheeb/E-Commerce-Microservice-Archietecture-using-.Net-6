 using Catalog.Api.Data;
using Catalog.Api.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Catalog.Api.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        private readonly ILogger<ProductRepository> _logger;

        private readonly FilterDefinitionBuilder<Product> filterBuilder = Builders<Product>.Filter;

        public ProductRepository(
            ICatalogContext context,
            ILogger<ProductRepository> logger
            )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }
       

        public async Task CreateProduct(Product product)
        {
             await _context.Products.InsertOneAsync(product);
           
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var result = await _context.Products.DeleteOneAsync(id);
            if (result == null)
            {
                _logger.LogError($" Error in the DeleteProduct method, {nameof(result)} ");
                throw new ArgumentNullException($" Error in DeleteProduct method {nameof(result)}");
            }

            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            var result = await _context.Products.FindAsync(filterBuilder.Eq(p => p.Category, categoryName));
            if (result == null)
            {
                _logger.LogError($" Error in the GetProductByCategory method, {nameof(result)} ");
                throw new ArgumentNullException($" Error in GetProductByCategory method {nameof(result)}");
            }

            return await result.ToListAsync();
        }

        public async Task<Product> GetProductById(string id)
        {
            var result = await _context.Products.FindAsync(filterBuilder.Eq(p => p.Id, id));
            if (result == null)
            {
                _logger.LogError($" Error in the GetProductByID method, {nameof(result)} ");
                throw new ArgumentNullException($" Error in GetProductById method {nameof(result)}");
            }

            return await  result.FirstOrDefaultAsync();

        }

        public async  Task<IEnumerable<Product>> GetProductByName(string name)
        {
            var result = await _context.Products.FindAsync(filterBuilder.ElemMatch(p => p.Name, name));
            if (result == null)
            {
                _logger.LogError($" Error in the GetProductByName method, {nameof(result)} ");
                throw new ArgumentNullException($" Error in GetProductByName method {nameof(result)}");
            }

            return await result.ToListAsync();
        }

        public async  Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<bool> UpdateProduct(string id , Product product)
        {
            var result = await _context.Products.ReplaceOneAsync(filterBuilder.Eq(p => p.Id, id), product);
            if (result == null)
            {
                _logger.LogError($" Error in the UpdateProduct method, {nameof(result)} ");
                throw new ArgumentNullException($" Error in UpdateProduct method {nameof(result)}");
            }

            return result.IsAcknowledged && result.ModifiedCount > 0;

        }

      
    }
}
