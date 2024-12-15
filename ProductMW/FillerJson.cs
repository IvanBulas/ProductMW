using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProductMW
{
    public class FillerJson
    {
        private readonly HttpClient _httpClient;
        public FillerJson(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Product>>("\"https://dummyjson.com/products\"");
            
        }
        public async Task<IEnumerable<String>> GetProductCategoriesAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<string>>("https://dummyjson.com/products/categories");

        }
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<User>>($"https://dummyjson.com/users");
        }
    }
}
