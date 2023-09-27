namespace Domain.Enums
{
    /// <summary>
    /// Represents the status of an order.
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// The order has been delivered.
        /// </summary>
        delivered = 1,

        /// <summary>
        /// The order has been canceled.
        /// </summary>
        canceled,

        /// <summary>
        /// The order is pending.
        /// </summary>
        pending,
    }

}
