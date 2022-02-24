using sample_distributedCache_redis.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sample_distributedCache_redis.Services
{
    public interface IProductService
    {
        Task<Product> GetProduct(int id);
        Task<List<Product>> GetAllProducts();
    }
}
