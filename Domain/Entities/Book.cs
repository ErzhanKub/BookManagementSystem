namespace Domain.Entities
{
    /// <summary>
    /// The class represents a book.
    /// Класс представляет книгу.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// A unique identifier for a book of type Guid.
        /// Уникальный идентификатор книги типа Guid.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// The title of the book is of type string. 
        /// This property is required.
        /// Название книги типа string. 
        /// Это свойство обязательно для заполнения.
        /// </summary>
        public required string Title { get; set; }
        /// <summary>
        /// Description of a book of type string?. 
        /// This property can be empty.
        /// Описание книги типа string?. 
        /// Это свойство может быть пустым.
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// The price of a book of type decimal.
        /// Цена книги типа decimal.
        /// </summary>
        public decimal Price { get; set; }
    }
}
