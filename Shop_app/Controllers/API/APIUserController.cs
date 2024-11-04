using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop_app.Models;

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
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model ...");
            }
            // Создаем нового пользователя IdentityUser с указанным email
            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            // Используем UserManager для создания пользователя с переданным паролем
            var result = await _userManager.CreateAsync(user, model.Password);

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
        public async Task<IActionResult> Auth([FromBody] LoginModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model ...");
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
                model.Email,
                model.Password,
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
