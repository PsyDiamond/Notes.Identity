using Microsoft.AspNetCore.Identity;

namespace Notes.Identity.Models
{
    /// <summary>
    /// Пользователь этого приложения (в добавок к стандартным полям)
    /// </summary>
    public class AppUser : IdentityUser
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }
    }
}
