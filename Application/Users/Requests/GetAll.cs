using Application.Users.Dtos;
using Domain.Repositories;
using MediatR;

namespace Application.Users.Requests
{
    /// <summary>
    /// Request class to get all users.
    /// Класс запроса на получение всех пользователей.
    /// </summary>
    public record GetAllUsersQuery : IRequest<IEnumerable<UserDto>> { }
    /// <summary>
    /// Request handler for getting all users.
    /// Обработчик запроса на получение всех пользователей.
    /// </summary>
    internal class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        /// <summary>
        /// Class constructor.
        /// Конструктор класса.
        /// </summary>
        /// <param name="userRepository"></param>
        public GetAllUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        /// <summary>
        /// Processing a request to get all users.
        /// Обработка запроса на получение всех пользователей.
        /// </summary>
        /// <param name="request">Request to get all users. Запрос на получение всех пользователей.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>A collection of objects of type UserDto. Коллекция объектов типа UserDto.</returns>
        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync().ConfigureAwait(false);
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
