using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO
{
    public class PostsWithoutImageDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
