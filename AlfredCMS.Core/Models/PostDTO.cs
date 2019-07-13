using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlfredCMS.Core.Models
{
    public partial class PostDTO
    {
        public int CategoryId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Slug { get; set; }

        public string Img { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Excerpt { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Updated { get; set; }
        
        public UserDTO User { get; set; }
    }
}
