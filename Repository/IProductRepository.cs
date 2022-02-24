using sample_distributedCache_redis.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sample_distributedCache_redis.Repository
{
    public interface IProductRepository
    {
        Task<Product> GetProduct(int id);
        Task<List<Product>> GetAllProducts();
    }
}
