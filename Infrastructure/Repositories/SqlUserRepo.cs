using Domain.DTO;
using Domain.Interfaces;
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
    public class SqlUserRepo : IUserRepo
    {
        private readonly AppDbContext _context;

        public SqlUserRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            
            user.CreatedAt = DateTime.Now;
            await _context.Users.AddAsync(user);
            return user;
        }

        public async Task<UsersWithCountDto> GetAllUsersAsync(int? pageNumber)
        {
            int pageSize = 10;
            int page = pageNumber ?? 1;
            var count = _context.Users.Count();
            var users = await _context.Users.Skip(((page - 1) * pageSize)).Take(pageSize).ToListAsync();
            UsersWithCountDto list = new UsersWithCountDto();
            list.Users = users;
            list.Count = count;
            return list;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<User> GetUserByUsernameAndPasswordAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
