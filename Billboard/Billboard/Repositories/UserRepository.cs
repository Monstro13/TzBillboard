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

namespace Billboard.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository() : base("users") { }

        internal bool FindUser(string login)
        {
            var user = Collection.AsQueryable<User>().FirstOrDefault(u => u.Login == login);

            return user != null;
        }

        internal User getUser(string login, string p)
        {
            return Collection.AsQueryable<User>().FirstOrDefault(u => u.Login == login && u.Password == p);
        }

        internal User getUserById(string userId)
        {
            return Collection.AsQueryable<User>().FirstOrDefault(u => u._id == new ObjectId(userId));
        }
    }
}