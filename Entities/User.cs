using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace newProject.Entities;

public partial class User
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string UserName { get; set; } = null!;

    [StringLength(255)]
    public string Password { get; set; } = null!;

    [StringLength(50)]
    public string? Email { get; set; }

    public bool? IsAdmin { get; set; }

    [Column("isActive")]
    public bool? IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    [InverseProperty("User")]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [InverseProperty("User")]
    public virtual ICollection<UserProfile> UserProfiles { get; set; } = new List<UserProfile>();
}
