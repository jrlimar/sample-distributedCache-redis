using sample_distributedCache_redis.Models;
using sample_distributedCache_redis.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sample_distributedCache_redis.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var products = await productRepository.GetAllProducts();

            return products;
        }

        public async Task<Product> GetProduct(int id)
        {
            var product = await productRepository.GetProduct(id);

            return product;
        }
    }
}
