using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AlfredCMS.Core.Models
{
    public class PostDTO
    {
        [Required, MaxLength(64)]
        public string Slug { get; set; }

        [Required, MaxLength(256)]
        public string Title { get; set; }

        [Required, MaxLength(256)]
        public string Excerpt { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
