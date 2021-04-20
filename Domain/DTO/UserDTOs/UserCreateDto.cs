using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO
{
    public class UserCreateDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
