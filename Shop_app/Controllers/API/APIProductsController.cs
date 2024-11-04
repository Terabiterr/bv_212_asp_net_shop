using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_app.Services;

namespace Shop_app.Controllers.API
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class APIProductsController : ControllerBase
    {
        private readonly IServiceProduct _serviceProduct;
        public APIProductsController(IServiceProduct serviceProduct)
        {
            _serviceProduct = serviceProduct;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _serviceProduct.ReadAsync();
            return Ok(products);
        }
    }
}
