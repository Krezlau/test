using AutoMapper;
using jobtest.Models;
using jobtest.Repositories;

namespace jobtest.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> _userRepository;
    private readonly IMapper _mapper;

    public UserService(IRepository<User> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await _userRepository.GetAsync(u => u.Id == id);
    }

    public async Task<User?> GetUserByNameAsync(string name)
    {
        return await _userRepository.GetAsync(u => u.Name == name);
    }

    public async Task<PageResultDTO<User>> GetUsersAsync(int page, int pageSize)
    {
        return await _userRepository.GetPageAsync(page, pageSize);
    }

    public async Task<Guid> CreateUserAsync(UserDTO user)
    {
        var newUser = _mapper.Map<User>(user);
        await _userRepository.CreateAsync(newUser);
        return newUser.Id;
    }

    public async Task UpdateUserAsync(Guid id, UserDTO user)
    {
        var existingUser = await _userRepository.GetAsync(u => u.Id == id);
        if (existingUser is null)
        {
            throw new Exception("User not found");
        }
        _mapper.Map(user, existingUser);
        await _userRepository.UpdateAsync(existingUser);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var existingUser = await _userRepository.GetAsync(u => u.Id == id);
        if (existingUser is null)
        {
            throw new Exception("User not found");
        }
        await _userRepository.DeleteAsync(existingUser);
    }
}