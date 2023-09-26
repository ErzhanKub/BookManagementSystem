using Domain.Enums;

namespace Application.Users.Dtos
{
    public record UserDto
    {
        public Guid Id { get; init; }
        public required string Username { get; init; }
        public required Role Role { get; init; }
    }
}
