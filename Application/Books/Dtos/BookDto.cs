namespace Application.Books.Dtos
{
    /// <summary>
    /// Class representing a data transfer object for a book.
    /// </summary>
    public record BookDto
    {
        /// <summary>
        /// The unique identifier of the book.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// The title of the book.
        /// </summary>
        public required string Title { get; init; }

        /// <summary>
        /// The description of the book.
        /// </summary>
        public string? Description { get; init; }

        /// <summary>
        /// The price of the book.
        /// </summary>
        public decimal Price { get; init; }
    }

}
