using subscription_Domain.Common;
using subscription_Domain.Entities;

namespace subscription_Domain.Repositories;

public interface IUserRepository 
{
    Task<User> GetByIdAsync(Guid id);
    Task<User> GetByEmailAsync(string email);
    Task<IEnumerable<User>> GetAllAsync();
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(Guid id);
}