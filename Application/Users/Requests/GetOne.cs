using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;

namespace Application.Users.Requests
{
    /// <summary>
    /// The response class to a user's get request.
    /// Класс ответа на запрос получения пользователя.
    /// </summary>
    public record GetUserResponse
    {
        /// <summary>
        /// User ID.
        /// Идентификатор пользователя.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public required string Username { get; set; }
        /// <summary>
        /// Роль пользователя.
        /// </summary>
        public required Role Role { get; init; }
        /// <summary>
        /// Корзина пользователя.
        /// </summary>
        public Basket? Basket { get; set; }
    }
    /// <summary>
    /// Request class to get user by name.
    /// Класс запроса на получение пользователя по имени.
    /// </summary>
    public record GetUserByNameQuery : IRequest<GetUserResponse?> { public required string Username { get; init; } }
    /// <summary>
    /// Request handler for retrieving a user by name.
    /// Обработчик запроса на получение пользователя по имени.
    /// </summary>
    internal class GetUserByNameHandler : IRequestHandler<GetUserByNameQuery, GetUserResponse?>
    {
        private readonly IUserRepository _userRepository;
        /// <summary>
        /// Class constructor.
        /// Конструктор класса.
        /// </summary>
        /// <param name="userRepository"></param>
        public GetUserByNameHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        /// <summary>
        /// Processing a request to get a user by name.
        /// Обработка запроса на получение пользователя по имени.
        /// </summary>
        /// <param name="request">Request to get user. Запрос на получение пользователя.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>An object of type GetUserResponse or null. Объект типа GetUserResponse или null.</returns>
        public async Task<GetUserResponse?> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByNameAsync(request.Username).ConfigureAwait(false);
            if (user is not null)
            {
                var response = new GetUserResponse
                {
                    Id = user.Id,
                    Username = user.Username,
                    Role = user.Role,
                    Basket = user.Basket,
                };
                return response;
            }
            return default;
        }
    }
}
