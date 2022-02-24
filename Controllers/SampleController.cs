using Microsoft.AspNetCore.Mvc;
using sample_distributedCache_redis.Services;
using System.Threading.Tasks;

namespace sample_distributedCache_redis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SampleController : ControllerBase
    {
        private readonly IProductService productService;

        public SampleController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var product = await productService.GetProduct(id);

            if (product is null) 
                return NotFound();

            return Ok(product);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var products = await productService.GetAllProducts();

            return Ok(products);
        }
    }
}
