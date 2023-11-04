using jobtest.DbContext;
using jobtest.Models;
using Microsoft.EntityFrameworkCore;

namespace jobtest.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User?> GetUserByNameAsync(string name)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Name == name);
    }

    public async Task<IEnumerable<User>> GetUsersAsync(int page, int pageSize)
    {
        return await _context.Users.Skip(page * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<Guid> CreateUserAsync(User user)
    {
        await _context.AddAsync(user);
        await _context.SaveChangesAsync();
        return user.Id;
    }

    public async Task UpdateUserAsync(User user)
    {
        _context.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(User user)
    {
        _context.Remove(user);
        await _context.SaveChangesAsync();
    }
}