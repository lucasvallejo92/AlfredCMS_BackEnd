using System;
using System.Collections.Generic;

namespace AlfredCMS.Data.Models
{
    public partial class Users
    {
        public Users()
        {
            Posts = new HashSet<Posts>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Posts> Posts { get; set; }
    }
}
