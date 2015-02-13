using Billboard.Repositories.DTO;
using Billboard.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billboard.Services.Interfaces
{
    public interface IUserService
    {
        SignUpResult RegisterUser(string login, string password);
        SignUpResult Login(string login, string password);
        CatUser GetUser(string userId);
        bool AddPost(String userId, String title, String text, String path);
        IEnumerable<CatPost> GetPosts(string userId, string page = "", string query = "");
        DeleteResult DeletePost(string postId);
        bool SavePost(string id, string title, string text);
        List<FriendInfo> GetFriends(string userId);
        List<CatPost> GetPostsByLogin(string login);
        List<CatUser> GetAllUsers();
        bool AddToFriend(string p, string id);
    }
}
