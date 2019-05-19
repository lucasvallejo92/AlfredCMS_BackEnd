﻿using AlfredCMS.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AlfredCMS.Core.Models
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [Required, MaxLength(64)]
        public string Name { get; set; }

        [Required, MaxLength(64)]
        public string Slug { get; set; }

        [MaxLength(256)]
        public string Description { get; set; }

        public List<Post> Post { get; set; }
    }
}
