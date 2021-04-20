using Domain.DTO;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Interfaces
{
    public interface ICommentRepo
    {
        Task<bool> SaveChangesAsync();
        Task<CommentsWithCountDto> GetAllCommentsAsync(int? pageNumber, int? userId, int? postId);
        Task<Comment> GetCommentByIdAsync(int id);
        Task<Comment> CreateCommentAsync(Comment comment);
        void UpdateComment(Comment comment);
        void DeleteComment(Comment comment);

    }
}
