namespace Domain.Entities
{
    /// <summary>
    /// A class that represents a book basket.
    /// Класс, представляющий корзину для книг.
    /// </summary>
    public class Basket
    {
        /// <summary>
        /// A unique Basket identifier of type Guid.
        /// Уникальный идентификатор корзины типа Guid.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Unique user identifier of type Guid.
        /// Уникальный идентификатор пользователя типа Guid.
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// List of books of type List<Book>. 
        /// By default, a list is initialized to an empty list.
        /// Список книг типа List<Book>. 
        /// По умолчанию список инициализируется пустым списком.
        /// </summary>
        public List<Book> Books { get; set; } = new();
    }
}
