using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Notes.Identity.Models;

namespace Notes.Identity.Data
{
    /// <summary>
    /// Контекст данных для аутентификации и авторизации
    /// </summary>
    public class AuthDbContext : IdentityDbContext<AppUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options) { }

        /// <summary>
        /// Действия при создании модели
        /// </summary>
        /// <param name="builder">построитель модели</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Сначала работает базовый класс
            base.OnModelCreating(builder);
            // По умолчанию именование: имя+AspNet. Это не устраивает - делаем мапинг
            RemapTables(builder);
            // Применить конфигурацию
            builder.ApplyConfiguration(new AppUserConfiguration());

        }

        /// <summary>
        /// Выполнить маппинг на таблицы
        /// </summary>
        /// <param name="builder">построитель модели</param>
        private static void RemapTables(ModelBuilder builder)
        {
            // Маппинг пользователей
            builder.Entity<AppUser>(entity => entity.ToTable(name: "Users"));
            // Маппинг ролей
            builder.Entity<IdentityRole>(entity => entity.ToTable(name: "Roles"));
            // Маппинг соспоставления пользователя и роли
            builder.Entity<IdentityUserRole<string>>(entity => entity.ToTable(name: "UserRoles"));
            // Маппинг прав пользователей
            builder.Entity<IdentityUserClaim<string>>(entity => entity.ToTable(name: "UserClaim"));
            // Маппинг логинов пользователей
            builder.Entity<IdentityUserLogin<string>>(entity => entity.ToTable("UserLogins"));
            // Маппинг токенов пользователей
            builder.Entity<IdentityUserToken<string>>(entity => entity.ToTable("UserTokens"));
            // Маппинг прав ролей
            builder.Entity<IdentityRoleClaim<string>>(entity => entity.ToTable("RoleClaims"));
        }
    }
}
