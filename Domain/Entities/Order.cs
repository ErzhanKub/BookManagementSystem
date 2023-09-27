using Domain.Enums;

namespace Domain.Entities
{
    /// <summary>
    /// Represents an order placed by a user.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Gets or sets the ID of the order.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who placed the order.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the address where the order should be delivered.
        /// </summary>
        public required string Adress { get; set; }

        /// <summary>
        /// Gets or sets the total price of the order.
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Gets or sets the status of the order.
        /// </summary>
        public OrderStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the list of books in the order.
        /// </summary>
        public List<Book> Books { get; set; } = new();

        /// <summary>
        /// Gets or sets the user who placed the order.
        /// </summary>
        public User? User { get; set; }
    }

}
