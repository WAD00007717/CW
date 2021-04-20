using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.DTO
{
    public class PostUpdateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
