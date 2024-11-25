using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Shop_app.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : ControllerBase
    {
        private readonly ShopContext _shopContext;
        public OrdersController(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _shopContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .ToListAsync();
            if (orders != null)
                return Ok(orders);
            else 
                return BadRequest("Not exists orders ...");
        }
    }
}
