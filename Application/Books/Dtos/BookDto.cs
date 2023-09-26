namespace Application.Books.Dtos
{
    public record BookDto
    {
        public Guid Id { get; init; }
        public required string Title { get; init; }
        public string? Description { get; init; }
        public decimal Price { get; init; }
    }
}
