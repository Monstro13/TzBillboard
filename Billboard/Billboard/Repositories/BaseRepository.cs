using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Billboard.Repositories
{
    public class BaseRepository<T> where T : class
    {
        public MongoCollection Collection { get; set; }

        public BaseRepository() { }

        public BaseRepository(String entities)
        {
            MongoServer server = MongoServer.Create(ConfigurationManager.AppSettings["mongoConnection"]);
            MongoDatabase Db = server.GetDatabase("billboard");

            MongoCollection<T> collection = Db.GetCollection<T>(entities);
            Collection = collection;
        }
    }
}