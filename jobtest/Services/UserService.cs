using AutoMapper;
using jobtest.Models;
using jobtest.Repositories;

namespace jobtest.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await _userRepository.GetUserByIdAsync(id);
    }

    public async Task<User?> GetUserByNameAsync(string name)
    {
        return await _userRepository.GetUserByNameAsync(name);
    }

    public async Task<IEnumerable<User>> GetUsersAsync(int page, int pageSize)
    {
        return await _userRepository.GetUsersAsync(page, pageSize);
    }

    public async Task<Guid> CreateUserAsync(UserDTO user)
    {
        var newUser = _mapper.Map<User>(user);
        return await _userRepository.CreateUserAsync(newUser);
    }

    public async Task UpdateUserAsync(Guid id, UserDTO user)
    {
        var existingUser = await _userRepository.GetUserByIdAsync(id);
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }
        _mapper.Map(user, existingUser);
        await _userRepository.UpdateUserAsync(existingUser);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var existingUser = await _userRepository.GetUserByIdAsync(id);
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }
        await _userRepository.DeleteUserAsync(existingUser);
    }
}