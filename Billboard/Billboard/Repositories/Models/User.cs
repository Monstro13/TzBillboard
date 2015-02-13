using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billboard.Repositories.Models
{
    public class User
    {
        public ObjectId _id { get; set; }

        public String Login { get; set; }

        public String Password { get; set; }

        public List<ObjectId> Friends { get; set; }
    }
}