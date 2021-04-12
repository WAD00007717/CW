using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Comment
    {
        public int PostID { get; set; }
        public Post post { get; set; }
        public int Id { get; set; }
    }
}
