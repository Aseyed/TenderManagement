using TenderManagement.Domain.Entities;

namespace TenderManagement.Application.Interfaces.Services;

public interface IAuthService
{
    Task RegisterUserAsync(string username, string password, string role);
    Task<string> LoginUserAsync(string username, string password);
    Task<User> GetUserByUsernameAsync(string username); 
}
