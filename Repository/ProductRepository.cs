using Microsoft.Extensions.Caching.Distributed;
using sample_distributedCache_redis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace sample_distributedCache_redis.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> products;
        private readonly IDistributedCache distributedCache;

        private readonly string key_all_products = "key_all_products";
        private readonly string key_product = "key_product";

        public ProductRepository(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;

            products = new List<Product>()
            {
                new Product(1, "teste1"),
                new Product(2, "teste2"),
                new Product(3, "teste3"),
                new Product(4, "teste4"),
                new Product(5, "teste5"),
            };  
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var cache = distributedCache.GetString(key_all_products);

            if (cache != null)
            {
                return JsonSerializer.Deserialize<List<Product>>(cache);
            }

            var toCache = JsonSerializer.Serialize(products);

            distributedCache.SetString(key_all_products, toCache, GetDistributedCacheEntryOptions(60, 120));

            return await Task.FromResult(products);
        }

        public async Task<Product> GetProduct(int id)
        {
            var cache = distributedCache.GetString(key_product);

            if (cache != null)
            {
                return JsonSerializer.Deserialize<Product>(cache);
            }

            var product = products.Where(x => x.Id == id).FirstOrDefault();

            if (product is not null)
            {
                var toCache = JsonSerializer.Serialize(product);

                distributedCache.SetString(key_product, toCache, GetDistributedCacheEntryOptions(60, 120));
            }

            return await Task.FromResult(product);
        }

        private DistributedCacheEntryOptions GetDistributedCacheEntryOptions(int slidingExpirationSecs = 0, int absoluteExpirationSecs = 0)
        {
            var cacheOptions = new DistributedCacheEntryOptions();

            if (slidingExpirationSecs > 0)
                cacheOptions.SetSlidingExpiration(TimeSpan.FromSeconds(slidingExpirationSecs)); // inactive

            if (absoluteExpirationSecs > 0)
                cacheOptions.SetAbsoluteExpiration(TimeSpan.FromSeconds(absoluteExpirationSecs)); // absolute

            return cacheOptions;
        }
    }
}
