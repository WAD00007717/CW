using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO
{
    public class LoginDto
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
    }
}
