using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Shop_app.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        //Продовжити назначення ролі
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(
            UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(string roleName, object error)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return BadRequest("Role name is important ...");
            }
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if(roleExists)
            {
                return BadRequest("Role already exists ...");
            }
            if(User.Identity.IsAuthenticated)
            {
                var role = new IdentityRole { Name = roleName };
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return Ok($"The role: {role.Name} is created ...");
                }
                return BadRequest(Json(result.Errors));
            }
            return BadRequest("Role create error ...");
        }
        [HttpGet]
        public IActionResult Create()
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
            var user = new IdentityUser 
            { 
                UserName = email, 
                Email = email,
                EmailConfirmed = true
            };

            // Используем UserManager для создания пользователя с переданным паролем
            var result = await _userManager.CreateAsync(user, password);

            // Если создание прошло успешно
            if (result.Succeeded)
            {
                // Возвращаем успешный ответ с сообщением
                //return Ok("Пользователь создан.");
                return RedirectToAction("Index", "Home");
            }

            // Если возникли ошибки, добавляем их в ModelState для отображения пользователю
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            // Возвращаем ошибки, если не удалось создать пользователя
            return BadRequest(ModelState);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            // Проверяем, что email и пароль были переданы
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                // Если email или пароль отсутствуют, возвращаем ошибку
                return BadRequest("Email и пароль обязательны.");
            }
            var result = await _signInManager.PasswordSignInAsync(
                email, 
                password,
                isPersistent: false,
                lockoutOnFailure: false
                );
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return BadRequest("Email or password are error ...");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
