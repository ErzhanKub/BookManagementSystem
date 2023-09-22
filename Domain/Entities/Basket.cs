namespace Domain.Entities
{
    public class Basket
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public required User User { get; set; }
        public List<Book> Books { get; set; } = new();
    }
}
