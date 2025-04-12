using subscription_Application.DTOs.Users;
using subscription_Application.Interfaces.Services;
using subscription_Domain.Entities;
using subscription_Domain.Repositories;

namespace subscription_Application.Services;

public class UsersService : IUsersService
{
    private readonly IUserRepository _userRepository;

    public UsersService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UsersDto> GetUserByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user is not null
            ? new UsersDto(user)
            : throw new KeyNotFoundException($"User with id {id} not found.");
    }

    public async Task<UsersDto> GetByEmailAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        return user is not null
            ? new UsersDto(user)
            : throw new KeyNotFoundException($"User with email {email} not found.");
    }

    public async Task<IEnumerable<UsersDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(user => new UsersDto(user));
    }

    public async Task AddAsync(UsersDto user)
    {
        var newUser = new User()
        {
            Email = user.Email,
            UserName = user.UserName,
            PasswordHash = user.PasswordHash,
            DisplayName = user.DisplayName,
            IsActive = user.IsActive,
            Roles = user.Roles
        };
        await _userRepository.AddAsync(newUser);
    }

    public async Task UpdateAsync(UsersDto user)
    {
        var existingUser = await _userRepository.GetByIdAsync(user.Id);
        if (existingUser is null)
            throw new KeyNotFoundException($"User with id {user.Id} not found.");

        existingUser.Email = user.Email;
        existingUser.UserName = user.UserName;
        existingUser.PasswordHash = user.PasswordHash;
        existingUser.DisplayName = user.DisplayName;
        existingUser.IsActive = user.IsActive;
        existingUser.Roles = user.Roles;

        await _userRepository.UpdateAsync(existingUser);
    }

    public async Task DeleteAsync(UsersDto user)
    {
        var existingUser = await _userRepository.GetByIdAsync(user.Id);
        if (existingUser is null)
            throw new KeyNotFoundException($"User with id {user.Id} not found.");

        await _userRepository.DeleteAsync(existingUser);
    }
}