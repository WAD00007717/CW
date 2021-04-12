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
    public class SqlPostRepo : IPostRepo
    {
        private readonly AppDbContext _context;

        public SqlPostRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreatePost(Post post)
        {
            if (post == null)
            {
                throw new ArgumentNullException(nameof(post));
            }

            _context.Posts.Add(post);
        }

        public void DeletePost(Post post)
        {
            if (post == null)
            {
                throw new ArgumentNullException(nameof(post));
            }

            _context.Posts.Remove(post);
        }

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            return await _context.Posts.ToListAsync();
        }

        public Post GetPostById(int id)
        {
            return _context.Posts.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdatePost(Post post)
        {
            // Nothing
        }
    }
}
