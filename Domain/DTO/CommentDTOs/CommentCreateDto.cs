using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO
{
    public class CommentCreateDto
    {
        public string Content { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
    }
}
