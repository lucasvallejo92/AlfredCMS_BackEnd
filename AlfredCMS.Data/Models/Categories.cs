using System;
using System.Collections.Generic;

namespace AlfredCMS.Data.Models
{
    public partial class Categories
    {
        public Categories()
        {
            Posts = new HashSet<Posts>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Posts> Posts { get; set; }
    }
}
