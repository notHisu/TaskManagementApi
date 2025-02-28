using TaskManagementApi.Models;

namespace TaskManagementApi.Interfaces
{
    public interface IAuthenticationService
    {
        string GenerateToken(User user);
    }
}
