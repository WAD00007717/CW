
using System.Collections.Generic;
using SocialMedia.Models;

namespace SocialMedia.Data
{
    public class MockPostRepo : IPostRepo
    {
        public Post GetPostById(int id)
        {
            return new Post { Id = 1, Title = "Post no 1", Description = "test", Image = "/image" };
        }
        public IEnumerable<Post> GetPosts()
        {
            var posts = new List<Post>
            {
                new Post { Id = 1, Title="Post no 1", Description = "test", Image = "/image" },
                new Post { Id = 2, Title="Post no 2", Description = "test", Image = "/image" },
                new Post { Id = 3, Title="Post no 3", Description = "test", Image = "/image" },
            };
            return posts;
        }
    }
}
