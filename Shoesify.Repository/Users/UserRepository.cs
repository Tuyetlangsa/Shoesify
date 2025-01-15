using Microsoft.EntityFrameworkCore;
using Shoesify.Entities.Models;

namespace Shoesify.Repository.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly ShoesifyContext _context;

        public UserRepository(ShoesifyContext context)
        {
            this._context = context;
        }

        public User Add(User user)
        {
            // Check if the user already exists by email or username
            if (_context.Users.Any(e => e.Email == user.Email))
            {
                throw new InvalidOperationException("A user with this email already exists.");
            }

            if (_context.Users.Any(n => n.Name == user.Name))
            {
                throw new InvalidOperationException("A user with this username already exists.");
            }

            var userId = user.UserId;
            if (string.IsNullOrEmpty(userId))
            {
                user.UserId = GenerateShortUniqueId();
            }
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        private string GenerateShortUniqueId()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 10);
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(e => e.Email == email);
        }

        public User GetByUsername(string username)
        {
            return _context.Users.FirstOrDefault(n => n.Name == username);
        }

        public User GeyById(string id)
        {
            return _context.Users.FirstOrDefault(i => i.UserId == id);
        }

        public User Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            return user;
        }
    }
}
