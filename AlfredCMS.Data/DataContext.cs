﻿
using AlfredCMS.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AlfredCMS.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }
    }
}
