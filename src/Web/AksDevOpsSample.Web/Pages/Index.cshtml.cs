using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AksDevOpsSample.Web.Pages
{
    public class IndexModel : PageModel
    {
        public string UsersContent { get; set; }
        public string ProductsContent { get; set; }

        private readonly ILogger logger;
        private readonly IHttpClientFactory httpClientFactory;
        public string ProductsApiUrl { get; }
        public string UsersApiUrl { get; }

        public IndexModel(ILogger<IndexModel> logger, IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;
            ProductsApiUrl = $"http://{config["PRODUCTSAPIURL"]}";
            UsersApiUrl =  $"http://{config["USERSAPIURL"]}";
        }

        public async Task OnGet()
        {
            this.UsersContent = await this.GetUsersAsync();
            this.ProductsContent = await this.GetProductsAsync();

        }

        public async Task<string> GetProductsAsync()
        {
            var destinationUrl = ProductsApiUrl + "/api/products";
            try
            {
                var httpClient = this.httpClientFactory.CreateClient();
                var content = await httpClient.GetStringAsync(destinationUrl);
                return content;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error getting products from url {destinationUrl}", ex);
                return ex.ToString();
            }
        }

        public async Task<string> GetUsersAsync()
        {
            var destinationUrl = UsersApiUrl + "/api/users";
            try
            {
                var httpClient = this.httpClientFactory.CreateClient();
                var content = await httpClient.GetStringAsync(destinationUrl);
                return content;
            }
            catch (Exception ex)
            {
                logger.LogError($"Error getting users from url {destinationUrl}", ex);
                return ex.ToString();
            }
        }
    }
}
