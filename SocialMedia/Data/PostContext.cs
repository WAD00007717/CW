using Microsoft.EntityFrameworkCore;
using SocialMedia.Models;

namespace SocialMedia.Data
{
    public class PostContext : DbContext
    {
        public PostContext(DbContextOptions<PostContext> opt) : base(opt)
        {
        }

        public DbSet<Post> Posts { get; set; }
    }
}