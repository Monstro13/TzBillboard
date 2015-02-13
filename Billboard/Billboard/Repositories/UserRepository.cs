using Billboard.Repositories.Models;
using MongoDB.Driver.Builders;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using Billboard.Repositories.Interfaces;
using Billboard.Repositories.DTO;

namespace Billboard.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository() : base("users") { }

        public bool FindUser(string login)
        {
            var user = Collection.AsQueryable<User>().FirstOrDefault(u => u.Login == login);

            return user != null;
        }

        public User getUser(string login, string p)
        {
            return Collection.AsQueryable<User>().FirstOrDefault(u => u.Login == login && u.Password == p);
        }

        public User getUserById(string userId)
        {
            return Collection.AsQueryable<User>().FirstOrDefault(u => u._id == new ObjectId(userId));
        }

        public bool Insert(User user)
        {
            try
            {
                Collection.Save(user);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public List<User> GetFriends(string userId)
        {
            var user = Collection.AsQueryable<User>().FirstOrDefault(u => u._id == new ObjectId(userId));
            var friends = new List<User>();
            if (user.Friends != null)
            {
                user.Friends.ForEach(x =>
                {
                    var friend = Collection.AsQueryable<User>().FirstOrDefault(u => u._id == x);
                    friends.Add(friend);
                });
            }

            return friends;
        }


        public string GetUserIdByLogin(string login)
        {
            return Collection.AsQueryable<User>().FirstOrDefault(u => u.Login == login)._id.ToString();
        }


        public List<DTO.CatUser> GetAll()
        {
            return Collection.AsQueryable<User>().Select(x => new CatUser(x)).ToList();
        }


        public bool AddFriend(string p, string id)
        {
            try
            {
                var user = Collection.AsQueryable<User>().FirstOrDefault(u => u._id == new ObjectId(p));

                if (user.Friends == null)
                {
                    user.Friends = new List<ObjectId>();
                    user.Friends.Add(new ObjectId(id));
                }
                else
                {
                    var friendId = new ObjectId(id);
                    if (!user.Friends.Contains(friendId))
                    {
                        user.Friends.Add(friendId);
                        Collection.Save(user);
                    }
                    else return false;
                }

                Collection.Save(user);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}