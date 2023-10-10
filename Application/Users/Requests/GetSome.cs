using Application.Users.Dtos;
using Domain.Repositories;

namespace Application.Users.Requests
{
    /// <summary>
    /// Query class for getting a list of users by their names.
    /// Класс запроса для получения списка пользователей по их именам.
    /// </summary>
    public record GetSomeUsersQuery : IRequest<IEnumerable<UserDto>>
    {
        /// <summary>
        /// An array of usernames.
        /// Массив имен пользователей.
        /// </summary>
        public required string[] Usernames { get; init; }
    }
    /// <summary>
    /// A request handler for obtaining a list of users by their names.
    /// Обработчик запроса на получение списка пользователей по их именам.
    /// </summary>
    internal class GetSomeUsersHandler : IRequestHandler<GetSomeUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Class constructor.
        /// Конструктор класса.
        /// </summary>
        /// <param name="userRepository">Репозиторий пользователей.</param>
        public GetSomeUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        /// <summary>
        /// Processing a request to obtain a list of users by their names.
        /// Обработка запроса на получение списка пользователей по их именам.
        /// </summary>
        /// <param name="request">Request to obtain a list of users. Запрос на получение списка пользователей.</param>
        /// <param name="cancellationToken">Токен отмены.</param>
        /// <returns>List of objects of type UserDto. Список объектов типа UserDto.</returns>
        public async Task<IEnumerable<UserDto>> Handle(GetSomeUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetSomeUsersByNamesAsync(request.Usernames).ConfigureAwait(false);
            var response = new List<UserDto>();
            foreach (var user in users)
            {
                var result = new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Role = user.Role,
                };
                response.Add(result);
            }
            return response;
        }
    }
}
