using Billboard.Repositories;
using Billboard.Repositories.DTO;
using Billboard.Repositories.LkpEnums;
using Billboard.Repositories.Models;
using Billboard.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Billboard.Services
{
    public class UserService
    {
        internal SignUpResult RegisterUser(string login, string password)
        {
            var userRepo = new UserRepository();
            var salt = CreateSalt(10);

            var user = new User()
            {
                _id = new MongoDB.Bson.ObjectId(),
                Login = login,
                Password = CreatePasswordHash(password, salt)
            };

            try
            {
                if (!userRepo.FindUser(login))
                {
                    userRepo.Insert(user);
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

        internal SignUpResult Login(string login, string password)
        {
            var userRepo = new UserRepository();
            var salt = CreateSalt(10);

            var user = userRepo.getUser(login, CreatePasswordHash(password, salt));

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

        internal CatUser GetUser(string userId)
        {
            var userRepo = new UserRepository();

            return new CatUser(userRepo.getUserById(userId));
        }

        internal bool AddPost(String userId, String title, String text, String path)
        {
            var postRepo = new PostRepository();

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

                postRepo.Insert(post);
                return true;
            }
            catch
            {
                return false;
            }
        }

        internal IEnumerable<CatPost> GetPosts(string userId, string page = "", string query = "")
        {
            var postRepo = new PostRepository();

            if (query.Length == 0)
            {
                return postRepo.GetPostsByUserId(userId, page);
            }
            else return postRepo.GetPostsByQuery(page, query);
        }

        internal DeleteResult DeletePost(string postId)
        {
            var postRepo = new PostRepository();

            return postRepo.DeletePostById(postId);
        }

        internal bool SavePost(string id, string title, string text)
        {
            var postRepo = new PostRepository();

            return postRepo.UpdatePostById(id, title, text);
        }
    }
}