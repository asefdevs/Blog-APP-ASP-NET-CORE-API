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

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:conn");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.Property(e => e.UserID).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.UserID)
                .HasConstraintName("FK_Blogs_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC0780FC72B9");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("((0))")
                .HasColumnName("isActive");
            entity.Property(e => e.IsAdmin).HasDefaultValueSql("((0))");
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
