using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // Импортирует пространство имен для работы с Identity и Entity Framework Core
using Microsoft.EntityFrameworkCore; // Импортирует пространство имен для работы с Entity Framework Core

// Контекст базы данных для пользователей, наследующий от IdentityDbContext
public class UserContext : IdentityDbContext
{
    // Конструктор принимает параметры конфигурации контекста базы данных
    public UserContext(DbContextOptions<UserContext> options)
        : base(options) // Передает параметры в базовый класс
    {
        // Базовый класс IdentityDbContext настроит таблицы для идентификации пользователей
    }
}
