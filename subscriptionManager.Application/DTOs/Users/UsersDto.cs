using subscription_Domain.Common;
using subscription_Domain.Entities;

namespace subscription_Application.DTOs.Users;

public class UsersDto(User user) : BaseEntities
{
    public string Email { get; set; } = user.Email;
    public string UserName { get; set; } = user.UserName;
    public string PasswordHash { get; set; } = user.PasswordHash;
    public string DisplayName { get; set; } = user.DisplayName;
    public bool IsActive { get; set; } = user.IsActive;
    public string[] Roles { get; set; } = user.Roles;
}