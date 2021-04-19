using Domain.DTO;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepo
    {
        Task<bool> SaveChangesAsync();
        Task<UsersWithCountDto> GetAllUsersAsync(int? pageNumber);
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUsernameAndPasswordAsync(string username);
        Task<User> CreateUserAsync(User user);
    }
}
