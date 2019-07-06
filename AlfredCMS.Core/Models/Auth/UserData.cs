using System.ComponentModel.DataAnnotations;

namespace AlfredCMS.Core.Models.Auth
{
    public class UserData
    {
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}
