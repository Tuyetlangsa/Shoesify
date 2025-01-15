using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shoesify.Entities.Models;
using Shoesify.Repository.Users;
using Microsoft.EntityFrameworkCore;

namespace Shoesify.Services.UserService
{
    
public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ShoesifyContext _context;

        public UserService(IUserRepository userRepository, ShoesifyContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        public List<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetUserById(string id)
        {
            return _userRepository.GeyById(id);
        }

        public User updateUser(User user)
        {
            if (user.UserId == null)
            {
                throw new InvalidOperationException("UserId cannot be null.");
            }

            var existingUser = _context.Users.FirstOrDefault(u => u.UserId == user.UserId);
            if (existingUser != null)
            {
                _context.Entry(existingUser).CurrentValues.SetValues(user);
            }
            else
            {
                _context.Users.Attach(user);
                _context.Entry(user).State = EntityState.Modified;
            }

            _context.SaveChanges();
            return user;
        }       
    }
}
