using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace newProject.Entities;

public partial class MyprojectdbContext : DbContext
{
    public MyprojectdbContext()
    {
    }

    public MyprojectdbContext(DbContextOptions<MyprojectdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:conn");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Category).WithMany(p => p.Blogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Blogs_Categories");

            entity.HasOne(d => d.User).WithMany(p => p.Blogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Blogs_Users");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Blog).WithMany(p => p.Comments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Blogs");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Users");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Blog).WithMany(p => p.Images)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Images_Blogs");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC0780FC72B9");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValueSql("((0))");
            entity.Property(e => e.IsAdmin).HasDefaultValueSql("((0))");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserProf__3214EC07FBD6AB0F");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.User).WithMany(p => p.UserProfiles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserProfi__UserI__3E1D39E1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
