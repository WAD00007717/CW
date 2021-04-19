using Domain.DTO;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Interfaces
{
    public interface IPostRepo
    {
        Task<bool> SaveChangesAsync();
        Task<PostsWithCountDto> GetAllPostsAsync(int? pageNumber, int? userId);
        Task<Post> GetPostByIdAsync(int id);
        Task<Post> CreatePostAsync(Post post);
        void UpdatePost(Post post);
        void DeletePost(Post post);

    }
}
