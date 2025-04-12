using Microsoft.AspNetCore.Mvc;
using subscription_Application.DTOs.Users;
using subscription_Application.Interfaces.Services;

namespace SubscriptionManager.API.Controller;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UsersDto>> GetById(Guid id)
    {
        try
        {
            var user = await _usersService.GetUserByIdAsync(id);
            return Ok(user);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpGet("email")]
    public async Task<ActionResult<UsersDto>> GetByEmail(string email)
    {
        try
        {
            var user = await _usersService.GetByEmailAsync(email);
            return Ok(user);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsersDto>>> GetAll()
    {
        try
        {
            var users = await _usersService.GetAllAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPost]
    public async Task<ActionResult> Add(UsersDto user)
    {
        try
        {
            await _usersService.AddAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, UsersDto user)
    {
        if (id != user.Id)
            return BadRequest("Les identifiants ne correspondent pas");
            
        try
        {
            await _usersService.UpdateAsync(user);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            var user = await _usersService.GetUserByIdAsync(id);
            await _usersService.DeleteAsync(user);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}