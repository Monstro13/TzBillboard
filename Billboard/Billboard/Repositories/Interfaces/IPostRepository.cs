using Billboard.Repositories.DTO;
using Billboard.Repositories.Models;
using Billboard.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billboard.Repositories.Interfaces
{
    public interface IPostRepository
    {
        IEnumerable<CatPost> GetPostsByUserId(string userId, string page = "");
        DeleteResult DeletePostById(string postId);
        bool UpdatePostById(string id, string title, string text);
        Post GetPostById(string postId);
        IEnumerable<CatPost> GetPostsByQuery(string page = "", string query = "");
        bool Insert(Post post);
        int GetCountPostsByUserId(MongoDB.Bson.ObjectId objectId);
    }
}
