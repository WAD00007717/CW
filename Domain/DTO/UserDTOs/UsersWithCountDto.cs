using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO
{
    public class UsersWithCountDto
    {
        public ICollection<User> Users { get; set; }
        public int Count { get; set; }
    }
}