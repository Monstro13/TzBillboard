using Billboard.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billboard.Repositories.DTO
{
    public class CatPost
    {
        public CatPost() { }

        public CatPost(Post post)
        {
            Id = post._id.ToString();
            Title = post.Title;
            Text = post.Text;
            ImagePath = post.ImagePath;
            DateCreation = post.DateCreation.ToString();
            UserLogin = new UserRepository().getUserById(post.UserId.ToString()).Login;
            UserId = post.UserId.ToString();
        }

        public String Id { get; set; }

        public String Title { get; set; }

        public String Text { get; set; }

        public String ImagePath { get; set; }

        public String DateCreation { get; set; }

        public String UserLogin { get; set; }

        public String UserId { get; set; }
    }
}