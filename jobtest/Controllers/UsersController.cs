using jobtest.Models;
using jobtest.Repositories;
using jobtest.Services;
using Microsoft.AspNetCore.Mvc;

namespace jobtest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : Controller
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUsersAsync([FromQuery] int page = 0, [FromQuery] int pageSize = 10)
    {
        var users = await _userService.GetUsersAsync(page, pageSize);
        return Ok(users);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserByIdAsync(Guid id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }
    
    [HttpGet("by-name/{name}")]
    public async Task<IActionResult> GetUserByNameAsync(string name)
    {
        var user = await _userService.GetUserByNameAsync(name);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromBody] UserDTO user)
    {
        var id = await _userService.CreateUserAsync(user);
        return Ok();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserAsync(Guid id, [FromBody] UserDTO user)
    {
        try
        {
            await _userService.UpdateUserAsync(id, user);
            return NoContent();
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserAsync(Guid id)
    {
        try
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}