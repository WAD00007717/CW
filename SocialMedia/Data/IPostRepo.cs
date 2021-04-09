using System.Collections.Generic;
using SocialMedia.Models;

namespace SocialMedia.Data
{
    public interface IPostRepo
    {
        IEnumerable<Post> GetPosts();
        Post GetPostById(int id);

    }
}
