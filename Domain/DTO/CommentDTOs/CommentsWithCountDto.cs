using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO
{
    public class CommentsWithCountDto
    {
        public ICollection<Comment> Comments { get; set; }
        public int Count { get; set; }
    }
}