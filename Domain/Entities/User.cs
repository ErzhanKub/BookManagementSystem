using Domain.Enums;

namespace Domain.Entities
{
    /// <summary>
    /// Represents the user.
    /// Представляет пользователя.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Unique user identifier of type Guid.
        /// Уникальный идентификатор пользователя типа Guid.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Username of type string. 
        /// The default value is empty.
        /// Имя пользователя типа string. 
        /// По умолчанию установлено пустое значение.
        /// </summary>
        public required string Username { get; set; }
        /// <summary>
        /// User password hash of type string. 
        /// The default value is empty.
        /// Хэш пароля пользователя типа string. 
        /// По умолчанию установлено пустое значение.
        /// </summary>
        public required string PasswordHash { get; set; }
        /// <summary>
        /// User role of type Role.
        /// Роль пользователя типа Role.
        /// </summary>
        public Role Role { get; set; }
        /// <summary>
        /// User's basket type Basket. 
        /// By default, a new cart is created.
        /// Корзина пользователя типа Basket. 
        /// По умолчанию создается новая корзина.
        /// </summary>
        public Basket Basket { get; set; } = new();
    }
}
