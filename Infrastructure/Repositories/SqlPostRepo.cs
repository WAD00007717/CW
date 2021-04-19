﻿using Domain.DTO;
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

        public async Task<Post> CreatePostAsync(Post post)
        {
            if (post == null)
            {
                throw new ArgumentNullException(nameof(post));
            }

            post.CreatedAt = DateTime.Now;
            await _context.Posts.AddAsync(post);
            return post;
        }

        public void DeletePost(Post post)
        {
            if (post == null)
            {
                throw new ArgumentNullException(nameof(post));
            }

            _context.Posts.Remove(post);
        }

        public async Task<PostsWithCountDto> GetAllPostsAsync(int? pageNumber, int? userId)
        {
            int pageSize = 10;
            int page = pageNumber ?? 1;
            var count = _context.Posts.Count();
            ICollection<Post> posts;
            if(userId != null)
            {
                posts = await _context.Posts.FromSqlRaw("select Id, Title, '' as Image, UserId, Description, CreatedAt from dbo.Posts").Include(p => p.User)
                                            .Where(p => p.UserId == userId).Skip(((page - 1) * pageSize)).Take(pageSize)
                                            .OrderByDescending(p => p.CreatedAt).ToListAsync();
            }

            posts = await _context.Posts.FromSqlRaw("select Id, Title, '' as Image, UserId, Description, CreatedAt from dbo.Posts").Include(p => p.User)
                                            .Skip(((page - 1) * pageSize)).Take(pageSize)
                                            .OrderByDescending(p => p.CreatedAt).ToListAsync();
           
            PostsWithCountDto list = new PostsWithCountDto();
            list.Posts = posts;
            list.Count = count;
            return list;
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            var post = await _context.Posts.Include(p => p.Comments).FirstOrDefaultAsync(p => p.Id == id);
            return post;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public void UpdatePost(Post post)
        {
            // Nothing
        }
    }
}
