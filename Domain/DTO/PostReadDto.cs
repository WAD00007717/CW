﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO
{
    public class PostReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
