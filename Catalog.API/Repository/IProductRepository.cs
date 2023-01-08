using Catalog.Api.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Api.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();

        Task<IEnumerable<Product>> GetProductByName(string name);
        Task<Product> GetProductById(string id);
        Task<IEnumerable<Product>> GetProductByCategory(string categoryName);

        Task  CreateProduct(Product product);
        Task<bool> UpdateProduct(string id , Product product);
        Task<bool> DeleteProduct(string id);


    } 
}
