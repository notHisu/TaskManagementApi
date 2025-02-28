using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;

namespace TaskManagementApi.Repositories
{
    public class UserRepository : IUserRepository<User>
    {
        private readonly TaskContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserRepository(TaskContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public User Add(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.PasswordHash = _passwordHasher.HashPassword(user, user.PasswordHash!);
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Delete(string id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User? GetById(string id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            return user;
        }

        public User Update(string id, User updatedUser)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            _context.Entry(user).CurrentValues.SetValues(updatedUser);
            _context.SaveChanges();

            return user;
        }
    }
}
