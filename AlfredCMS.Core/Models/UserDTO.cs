using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AlfredCMS.Core.Models
{
    public partial class UserDTO
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required, JsonIgnore]
        public string PasswordHash { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
