using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;
using System.Security.Cryptography;
using System.Text;

namespace TaskManagementApi.Repositories
{
    public class UserRepository : IUserRepository<UserResponseDto>
    {
        private readonly TaskContext _context;
        
        public UserRepository(TaskContext context) {
            _context = context;
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        private static UserResponseDto ToDto(User user)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };
        }

        public UserResponseDto Add(UserCreateDto createDto)
        {
            var user = new User
            {
                Username = createDto.Username,
                Email = createDto.Email,
                PasswordHash = HashPassword(createDto.Password)
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            
            return ToDto(user);
        }

        public void Delete(int id)
        {
            _context.Users.Remove(_context.Users.Find(id));
        }

        public IEnumerable<UserResponseDto> GetAll()
        {
            return _context.Users.Select(user => ToDto(user)).ToList();
        }

        public UserResponseDto? GetById(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            return user != null ? ToDto(user) : null;
        }

        public UserResponseDto? Update(int id, UserUpdateDto updateDto)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            if (user == null) return null;

            if (updateDto.Username != null)
                user.Username = updateDto.Username;
            
            if (updateDto.Email != null)
                user.Email = updateDto.Email;
            
            if (updateDto.Password != null)
                user.PasswordHash = HashPassword(updateDto.Password);

            _context.Users.Update(user);
            _context.SaveChanges();

            return ToDto(user);
        }

        public UserResponseDto? ValidateCredentials(string username, string password)
        {
            var hashedPassword = HashPassword(password);
            var user = _context.Users.FirstOrDefault(u => 
                u.Username == username && 
                u.PasswordHash == hashedPassword);
            
            return user != null ? ToDto(user) : null;
        }
    }
}