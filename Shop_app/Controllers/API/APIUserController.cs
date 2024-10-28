using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Shop_app.Controllers.API
{
    //This controller was created for our partners
    [Route("api/[controller]")]
    [ApiController]
    public class APIUserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public APIUserController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(string email, string password)
        {
            Console.WriteLine("I'm here!!!");
            // Перевіряємо, що email та пароль були передані
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
                return Ok("User register successfully ...");
            }

            // Возвращаем ошибки, если не удалось создать пользователя
            return BadRequest(result.Errors);
        }
        [HttpPost("auth")]
        public async Task<IActionResult> Auth(string email, string password)
        {
            // Проверяем, что email и пароль были переданы
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                // Если email или пароль отсутствуют, возвращаем ошибку
                return BadRequest("Email и пароль обязательны.");
            }
            /*
             isPersistent
            Boolean
            Флаг, указывающий, должен ли файл cookie для входа сохраняться после закрытия браузера.

            lockoutOnFailure
            Boolean
            Флаг, указывающий, должна ли учетная запись пользователя быть заблокирована в случае сбоя входа.
             */
            var result = await _signInManager.PasswordSignInAsync(
                email,
                password,
                isPersistent: false,
                lockoutOnFailure: false
                );
            if (result.Succeeded)
            {
                return Ok("Authed successfully ...");
            }
            return BadRequest("Invalid email or password...");
        }
        public async Task<IActionResult> AccessDenied()
        {
            return BadRequest("Cookie: Access Denied ...");
        }
    }
}
