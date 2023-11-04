using jobtest.Models;

namespace jobtest.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(Guid id);
    Task<User?> GetUserByNameAsync(string name);
    Task<IEnumerable<User>> GetUsersAsync(int page, int pageSize);
    Task<Guid> CreateUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(User user);
}