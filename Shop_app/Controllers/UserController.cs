using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Shop_app.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        public UserController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        // POST-метод для создания нового пользователя
        [HttpPost]
        public async Task<IActionResult> Create(string email, string password)
        {
            // Проверяем, что email и пароль были переданы
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                // Если email или пароль отсутствуют, возвращаем ошибку
                return BadRequest("Email и пароль обязательны.");
            }

            // Создаем нового пользователя IdentityUser с указанным email
            var user = new IdentityUser { UserName = email, Email = email };

            // Используем UserManager для создания пользователя с переданным паролем
            var result = await _userManager.CreateAsync(user, password);

            // Если создание прошло успешно
            if (result.Succeeded)
            {
                // Возвращаем успешный ответ с сообщением
                return Ok("Пользователь создан.");
            }

            // Если возникли ошибки, добавляем их в ModelState для отображения пользователю
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            // Возвращаем ошибки, если не удалось создать пользователя
            return BadRequest(ModelState);
        }


    }
}
