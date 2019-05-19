using AlfredCMS.Data.Models;
using Newtonsoft.Json;
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

        [Required]
        public string Img { get; set; }

        [Required, MaxLength(256)]
        public string Excerpt { get; set; }

        [Required]
        public string Content { get; set; }

        [JsonIgnore]
        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [JsonIgnore]
        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

        public DateTime Created { get; set; }
    }
}
