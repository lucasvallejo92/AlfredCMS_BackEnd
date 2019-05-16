using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AlfredCMS.Core.Models
{
    public class UserDTO
    {
        public int Id { get; set; }

        [Required, MaxLength(128)]
        public string UserName { get; set; }

        [Required, MaxLength(1024)]
        public string PasswordHash { get; set; }

        [Required, MaxLength(128)]
        public string Email { get; set; }

        [MaxLength(32)]
        public string FirstName { get; set; }

        [MaxLength(32)]
        public string LastName { get; set; }
    }
}
