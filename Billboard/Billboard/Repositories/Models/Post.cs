using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billboard.Repositories.Models
{
    public class Post
    {
        public ObjectId _id { get; set; }

        public String Title { get; set; }

        public String Text { get; set; }

        public String ImagePath { get; set; }

        public DateTime DateCreation { get; set; }

        public ObjectId UserId { get; set; }
    }
}