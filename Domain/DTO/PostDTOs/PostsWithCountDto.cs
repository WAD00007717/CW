using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO
{
    public class PostsWithCountDto
    {
        public ICollection<Post> Posts { get; set; }
        public int Count { get; set; }
    }
}
