using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlfredCMS.Data.Models
{
    [Table("Posts")]
    public class Post
    {
        [Key, Required]
        public int Id { get; set; }

        [Required, MaxLength(64)]
        public string Slug { get; set; }

        [Required]
        public string Img { get; set; }

        [Required, MaxLength(256)]
        public string Title { get; set; }

        [Required, MaxLength(256)]
        public string Excerpt { get; set; }

        [Required]
        public string Content { get; set; }

        [JsonIgnore]
        [Required]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [JsonIgnore]
        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public DateTime Created { get; set; }
    }
}
