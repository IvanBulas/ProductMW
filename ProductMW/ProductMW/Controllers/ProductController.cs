using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductMW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext  _appDbContext;
        public ProductController(AppDbContext appDbContext)
        { 
            _appDbContext = appDbContext;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts() => await _appDbContext.Product.ToListAsync();
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _appDbContext.Product.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Product>>> FilterProducts(string category, float price, string Description)
        {
            return await _appDbContext.Product.Where(x => x.Category == category).ToListAsync();
            var query = _appDbContext.Product.AsQueryable();
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.Category == category);
            }
            if (price > 0)
            {
                query = query.Where(p => p.Price == price);
            }
            if (!string.IsNullOrEmpty(Description))
            {
                query = query.Where(p => p.Description == Description);
            }
            return await query.ToListAsync();
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts(string search)
        {
            return await _appDbContext.Product.Where(p => p.Name.Contains(search)).ToListAsync();
        }
    }

     
}
