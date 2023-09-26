using Domain.Enums;

namespace Application.Users.Dtos
{
    /// <summary>
    /// Class representing a user data transfer object.
    /// </summary>
    public record UserDto
    {
        /// <summary>
        /// The unique identifier of the user.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// The username of the user.
        /// </summary>
        public required string Username { get; init; }

        /// <summary>
        /// The role of the user.
        /// </summary>
        public required Role Role { get; init; }
    }

}
