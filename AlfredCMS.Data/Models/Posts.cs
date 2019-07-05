using System;
using System.Collections.Generic;

namespace AlfredCMS.Data.Models
{
    public partial class Posts
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public string Slug { get; set; }
        public string Img { get; set; }
        public string Title { get; set; }
        public string Excerpt { get; set; }
        public string Content { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }

        public virtual Categories Category { get; set; }
        public virtual Users User { get; set; }
    }
}
