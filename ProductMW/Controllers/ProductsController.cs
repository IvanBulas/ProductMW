using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _appcontext;
        private readonly ILogger<ProductsController> _logger;
       public ProductsController(AppDbContext context, ILogger<ProductsController> logger) 
        { _appcontext = context;
            _logger = logger; 
        }
        [HttpGet]
        [HttpGet] public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        { 
            _logger.LogInformation("Fetching all products"); 
            return await _appcontext.Products.ToListAsync(); 
        }
        [HttpGet("{id}")] public async Task<ActionResult<Product>> GetProduct(int id) 
        { _logger.LogInformation($"Fetching product with id {id}"); 
            var product = await _appcontext.Products.FindAsync(id); 
            if (product == null)
            { 
                _logger.LogWarning($"Product with id {id} not found"); 
                return NotFound(); }
                return product; 
            }
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Product>>> FilterProducts(string category, decimal? minPrice, decimal? maxPrice)
        {
            var query = _appcontext.Products.AsQueryable();
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.Category == category);
            }
            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }
            return await query.ToListAsync();
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts(string searchText)
        {
            return await _appcontext.Products
                .Where(p => p.Name.Contains(searchText)).ToListAsync();
        }
    }
}
