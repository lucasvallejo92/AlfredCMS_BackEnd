using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlfredCMS.Data.Models
{
    [Table("Users")]
    public class User
    {
        [Key, Required]
        public int Id { get; set; }

        [Required, MaxLength(128)]
        public string UserName { get; set; }

        [JsonIgnore]
        [Required, MaxLength(1024)]
        public string PasswordHash { get; set; }

        [JsonIgnore]
        [Required, MaxLength(128)]
        public string Email { get; set; }

        [MaxLength(32)]
        public string FirstName { get; set; }

        [MaxLength(32)]
        public string LastName { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
