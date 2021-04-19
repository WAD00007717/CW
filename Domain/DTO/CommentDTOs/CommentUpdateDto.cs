using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO
{
    public class CommentUpdateDto
    {
        [Required]
        public string Content { get; set; }
    }
}
