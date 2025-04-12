using subscription_Application.DTOs.Users;
using subscription_Domain.Entities;

namespace subscription_Application.Interfaces.Services;

public interface IUsersService
{
    Task<UsersDto> GetUserByIdAsync(Guid id);

    Task<UsersDto> GetByEmailAsync(string email);

    Task<IEnumerable<UsersDto>> GetAllAsync();

    Task AddAsync(UsersDto user);

    Task UpdateAsync(UsersDto user);

    Task DeleteAsync(UsersDto user);
}