using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;
using System.Security.Cryptography;
using System.Text;

namespace TaskManagementApi.Repositories
{
    public class UserRepository : IUserRepository<UserResponseDto>
    {
        private readonly TaskContext _context;

        public UserRepository(TaskContext context)
        {
            _context = context;
        }

        private static string GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }

        private static string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = password + salt;
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private static UserResponseDto ToDto(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };
        }

        public UserResponseDto Add(UserCreateDto createDto)
        {
            if (createDto == null)
            {
                throw new ArgumentNullException(nameof(createDto));
            }

            var salt = GenerateSalt();
            var user = new User
            {
                Username = createDto.Username,
                Email = createDto.Email,
                PasswordHash = HashPassword(createDto.Password, salt),
                PasswordSalt = salt
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return ToDto(user);
        }

        public void Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            if (user != null)
            {
                _context.Users.Remove(_context.Users.Find(id)!);
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("User not found");
            }
        }

        public IEnumerable<UserResponseDto> GetAll()
        {
            return _context.Users.Select(user => ToDto(user)).ToList();
        }

        public UserResponseDto? GetById(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            if (user != null)
            {
                return ToDto(user);
            }
            else
            {
                throw new InvalidOperationException("User not found");
            }
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
            {
                var salt = GenerateSalt();
                user.PasswordHash = HashPassword(updateDto.Password, salt);
                user.PasswordSalt = salt;
            }

            _context.Users.Update(user);
            _context.SaveChanges();

            return ToDto(user);
        }

        public UserResponseDto? ValidateCredentials(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null) return null;

            var hashedPassword = HashPassword(password, user.PasswordSalt);
            if (user.PasswordHash != hashedPassword) return null;

            return ToDto(user);
        }
    }
}
