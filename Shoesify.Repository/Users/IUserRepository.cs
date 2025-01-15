using Shoesify.Entities.Models;

namespace Shoesify.Repository.Users
{
    public interface IUserRepository
    {
        User Add(User user);
        User Update(User user);
        User GeyById(string id);
        List<User> GetAll();
        User GetByEmail(string email);
        User GetByUsername(string username);
    }
}
