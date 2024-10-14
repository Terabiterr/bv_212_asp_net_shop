using Microsoft.AspNetCore.Mvc;
using Shop_app.Models;

namespace Shop_app.Controllers
{
    public class ProductsController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            return View();
        }
        [HttpGet]
        public ViewResult Create() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Description")] Product product)
        {
            return RedirectToAction(nameof(Index));    
        }
    }
}
