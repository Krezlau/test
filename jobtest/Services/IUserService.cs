using jobtest.Models;

namespace jobtest.Services;

public interface IUserService
{
    Task<User?> GetUserByIdAsync(Guid id);
    Task<User?> GetUserByNameAsync(string name);
    Task<IEnumerable<User>> GetUsersAsync(int page, int pageSize);
    Task<Guid> CreateUserAsync(UserDTO user);
    Task UpdateUserAsync(Guid id, UserDTO user);
    Task DeleteUserAsync(Guid id);
}