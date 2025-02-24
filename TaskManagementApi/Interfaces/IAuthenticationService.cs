using TaskManagementApi.Models;

namespace TaskManagementApi.Interfaces
{
    public interface IAuthenticationService
    {
        AuthResponseDto? Authenticate(string username,  string password);
        string GenerateToken(UserResponseDto user);
    }
}
