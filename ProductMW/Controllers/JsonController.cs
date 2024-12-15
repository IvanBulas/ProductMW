using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProductMW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JsonController : ControllerBase
    {

        private readonly FillerJson _fillerJsonService;

        public JsonController(FillerJson fillerJsonService)
        {
            _fillerJsonService = fillerJsonService;
        }

        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _fillerJsonService.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<string>>> GetProductCategories()
        {
            var categories = await _fillerJsonService.GetProductCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _fillerJsonService.GetUsersAsync();
            return Ok(users);
        }
    }
}
