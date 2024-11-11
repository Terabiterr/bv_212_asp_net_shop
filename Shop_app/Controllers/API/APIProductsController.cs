using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_app.Models;
using Shop_app.Services;
using System.ComponentModel.DataAnnotations;

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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _serviceProduct.GetByIdAsync(id);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            //validation from model/Product (attributes)
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productCreated = await _serviceProduct.CreateAsync(product);
            return CreatedAtAction("Created successfully", productCreated);
        }

    }
}
