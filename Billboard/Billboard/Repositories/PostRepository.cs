using Billboard.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using Billboard.Repositories.DTO;
using MongoDB.Driver.Builders;
using System.IO;
using Billboard.Services.Models;

namespace Billboard.Repositories
{
    public class PostRepository : BaseRepository<Post>
    {
        public PostRepository() : base("posts") { }

        internal IEnumerable<CatPost> GetPostsByUserId(string userId, string page = "")
        {
            var firstData = Collection.AsQueryable<Post>()
                                      .Where(p => p.UserId == new ObjectId(userId))
                                      .OrderByDescending(x => x.DateCreation)
                                      .Select(x => new CatPost(x))
                                      .ToList();

            if (page != "")
            {
                var p = 0;
                if (Int32.TryParse(page, out p))
                {
                    return firstData.Skip((p - 1) * 5).Take(5).ToList();
                }
                else return firstData;
            }
            else return firstData;
        }

        internal DeleteResult DeletePostById(string postId)
        {
            try
            {
                var post = GetPostById(postId);
                var image = post.ImagePath;

                var query = Query<Post>.EQ(p => p._id, new ObjectId(postId));
                Collection.Remove(query);

                return new DeleteResult
                {
                    FromDb = true,
                    ImagePath = image
                };
            }
            catch
            {
                return new DeleteResult
                {
                    FromDb = false,
                    ImagePath = ""
                };
            }
        }

        internal bool UpdatePostById(string id, string title, string text)
        {
            try
            {
                var query = Query<Post>.EQ(p => p._id, new ObjectId(id));
                var update = Update<Post>.Set(p => p.Title, title).Set(p => p.Text, text).Set(p => p.DateCreation, DateTime.Now);
                Collection.Update(query, update);
                return true;
            }
            catch { return false; }
        }

        internal Post GetPostById(string postId)
        {
            return Collection.AsQueryable<Post>().FirstOrDefault(p => p._id == new ObjectId(postId));
        }

        internal IEnumerable<CatPost> GetPostsByQuery(string page = "", string query = "")
        {
            query = query.ToLower().Trim();
            var firstData = Collection.AsQueryable<Post>()
                                      .Where(p => p.Title.ToLower().Trim().Contains(query))
                                      .OrderByDescending(x => x.DateCreation)
                                      .Select(x => new CatPost(x))
                                      .ToList();

            if (page != "")
            {
                var p = 0;
                if (Int32.TryParse(page, out p))
                {
                    return firstData.Skip((p - 1) * 5).Take(5).ToList();
                }
                else return firstData;
            }
            else return firstData;
        }
    }
}