using System.ComponentModel.DataAnnotations;

namespace AlfredCMS.Core.Models
{
    public partial class CategoryDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Slug { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
