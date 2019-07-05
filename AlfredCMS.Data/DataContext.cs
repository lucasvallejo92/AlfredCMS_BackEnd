using System;
using AlfredCMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AlfredCMS.Data.Models
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=localhost;port=32774;user=root;password=123456;database=alfred_cms");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.ToTable("categories", "alfred_cms");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasColumnName("slug")
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Posts>(entity =>
            {
                entity.ToTable("posts", "alfred_cms");

                entity.HasIndex(e => e.CategoryId)
                    .HasName("category_id_idx");

                entity.HasIndex(e => e.UserId)
                    .HasName("user_id_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CategoryId)
                    .HasColumnName("category_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .IsUnicode(false);

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Excerpt)
                    .IsRequired()
                    .HasColumnName("excerpt")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Img)
                    .HasColumnName("img")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasColumnName("slug")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Updated)
                    .HasColumnName("updated")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_post_category_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_post_user_id");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users", "alfred_cms");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasColumnName("password_hash")
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("user_name")
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });
        }
    }
}
