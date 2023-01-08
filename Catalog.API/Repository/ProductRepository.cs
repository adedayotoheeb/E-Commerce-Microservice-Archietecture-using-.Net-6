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
        private readonly FilterDefinitionBuilder<Product> filterBuilder = Builders<Product>.Filter;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }
       

        public async Task CreateProduct(Product product)
        {
             await _context.Products.InsertOneAsync(product);
           
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var result = await _context.Products.DeleteOneAsync(id);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            var result = await _context.Products.FindAsync(filterBuilder.Eq(p => p.Category, categoryName));

            return await result.ToListAsync();
        }

        public async Task<Product> GetProductById(string id)
        {
            var result = await _context.Products.FindAsync(filterBuilder.Eq(p => p.Id, id));

            return await  result.FirstOrDefaultAsync();

        }

        public async  Task<IEnumerable<Product>> GetProductByName(string name)
        {
            var result = await _context.Products.FindAsync(filterBuilder.ElemMatch(p => p.Name, name));

            return await result.ToListAsync();
        }

        public async  Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<bool> UpdateProduct(string id , Product product)
        {
            var result = await _context.Products.ReplaceOneAsync(filterBuilder.Eq(p => p.Id, id), product);

            return result.IsAcknowledged && result.ModifiedCount >0;

        }

      
    }
}
