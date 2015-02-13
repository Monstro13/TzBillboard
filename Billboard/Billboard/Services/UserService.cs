using Billboard.Repositories;
using Billboard.Repositories.DTO;
using Billboard.Repositories.Interfaces;
using Billboard.Repositories.LkpEnums;
using Billboard.Repositories.Models;
using Billboard.Services.Interfaces;
using Billboard.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Billboard.Services
{
    public class UserService : IUserService
    {
        private readonly IPostRepository _postRepository;

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IPostRepository postRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public SignUpResult RegisterUser(string login, string password)
        {
            var salt = CreateSalt(10);

            var user = new User()
            {
                _id = new MongoDB.Bson.ObjectId(),
                Login = login,
                Password = CreatePasswordHash(password, salt)
            };

            try
            {
                if (!_userRepository.FindUser(login))
                {
                    _userRepository.Insert(user);
                    return new SignUpResult
                    {
                        Code = StatusCode.Success,
                        User = new CatUser(user)
                    };
                }
                else
                {
                    return new SignUpResult
                    {
                        Code = StatusCode.UserIsExists,
                        User = null
                    };
                }
            }
            catch
            {
                return new SignUpResult
                {
                    Code = StatusCode.Fail,
                    User = null
                };
            }
        }

        private static string CreateSalt(int size)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }

        private static string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = String.Concat(pwd, salt);

            using (MD5 md5Hash = MD5.Create())
            {
                return GetMd5Hash(md5Hash, pwd);
            }
        }

        private static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public SignUpResult Login(string login, string password)
        {
            var salt = CreateSalt(10);
            var user = _userRepository.getUser(login, CreatePasswordHash(password, salt));

            if (user != null)
            {
                return new SignUpResult
                {
                    Code = StatusCode.Success,
                    User = new CatUser(user)
                };
            }
            else
            {
                return new SignUpResult
                {
                    Code = StatusCode.UserIsNotExists,
                    User = null
                };
            }
        }

        public CatUser GetUser(string userId)
        {
            return new CatUser(_userRepository.getUserById(userId));
        }

        public bool AddPost(String userId, String title, String text, String path)
        {
            try
            {
                var post = new Post
                {
                    _id = new MongoDB.Bson.ObjectId(),
                    Title = title,
                    Text = text,
                    ImagePath = path,
                    DateCreation = DateTime.Now,
                    UserId = new MongoDB.Bson.ObjectId(userId)
                };

                _postRepository.Insert(post);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<CatPost> GetPosts(string userId, string page = "", string query = "")
        {
            if (query.Length == 0)
            {
                return _postRepository.GetPostsByUserId(userId, page);
            }
            else return _postRepository.GetPostsByQuery(page, query);
        }

        public DeleteResult DeletePost(string postId)
        {
            return _postRepository.DeletePostById(postId);
        }

        public bool SavePost(string id, string title, string text)
        {
            return _postRepository.UpdatePostById(id, title, text);
        }

        public List<FriendInfo> GetFriends(string userId)
        {
            var friends = _userRepository.GetFriends(userId);
            var friendsList = new List<FriendInfo>();

            friends.ForEach(x =>
            {
                var info = new FriendInfo
                {
                    Login = x.Login,
                    CountPosts = _postRepository.GetCountPostsByUserId(x._id)
                };
                friendsList.Add(info);
            });

            return friendsList;
        }


        public List<CatPost> GetPostsByLogin(string login)
        {
            var userId = _userRepository.GetUserIdByLogin(login);

            return _postRepository.GetPostsByUserId(userId).ToList();
        }


        public List<CatUser> GetAllUsers()
        {
            return _userRepository.GetAll();
        }


        public bool AddToFriend(string p, string id)
        {
            return _userRepository.AddFriend(p, id);
        }
    }
}