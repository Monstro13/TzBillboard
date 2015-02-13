using Billboard.Repositories.DTO;
using Billboard.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billboard.Repositories.Interfaces
{
    public interface IUserRepository
    {
        bool FindUser(string login);
        User getUser(string login, string p);
        User getUserById(string userId);
        bool Insert(User user);
        List<User> GetFriends(string userId);
        string GetUserIdByLogin(string login);
        List<CatUser> GetAll();

        bool AddFriend(string p, string id);
    }
}
