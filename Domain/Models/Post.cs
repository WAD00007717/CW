﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(10)]
        public string Title { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
