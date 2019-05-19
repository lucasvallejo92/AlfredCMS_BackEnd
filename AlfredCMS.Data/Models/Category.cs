using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AlfredCMS.Data.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key, Required]
        public int Id { get; set; }

        [Required, MaxLength(64)]
        public string Name { get; set; }

        [Required, MaxLength(64)]
        public string Slug { get; set; }

        [MaxLength(256)]
        public string Description { get; set; }

        [JsonIgnore]
        public virtual List<Post> Post { get; set; }
    }
}
