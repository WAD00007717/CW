using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO
{
    public class UserReadDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
