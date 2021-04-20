using Domain.DTO;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SqlCommentRepo : ICommentRepo
    {
        private readonly AppDbContext _context;

        public SqlCommentRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            comment.CreatedAt = DateTime.Now;
            await _context.Comments.AddAsync(comment);
            return comment;
        }

        public void DeleteComment(Comment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            _context.Comments.Remove(comment);
        }

        public async Task<CommentsWithCountDto> GetAllCommentsAsync(int? pageNumber, int? userId, int? postId)
        {
            int pageSize = 10;
            int page = pageNumber ?? 1;
            var count = _context.Comments.Count();
            ICollection<Comment> comments;
            if (userId != null)
            {
                comments = await _context.Comments.Include(c => c.User).Where(c => c.UserId == userId)
                                        .Skip(((page - 1) * pageSize)).Take(pageSize)
                                        .OrderByDescending(c => c.CreatedAt).ToListAsync();
            }
            else if (postId != null)
            {
                comments = await _context.Comments.Include(c => c.User).Where(c => c.PostId == postId)
                                        .Skip(((page - 1) * pageSize)).Take(pageSize)
                                        .OrderByDescending(c => c.CreatedAt).ToListAsync();
            } else
            {
                comments = await _context.Comments.Include(c => c.User)
                                    .Skip(((page - 1) * pageSize)).Take(pageSize)
                                    .OrderByDescending(c => c.CreatedAt).ToListAsync();
            }
            
            CommentsWithCountDto list = new CommentsWithCountDto();
            list.Comments = comments;
            list.Count = count;
            return list;
        }

        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            return await _context.Comments.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public void UpdateComment(Comment comment)
        {
            // Nothing
        }
    }
}
