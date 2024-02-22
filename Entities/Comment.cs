using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace newProject.Entities;

public partial class Comment
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("blog_id")]
    public int BlogId { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("context")]
    public string Context { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("BlogId")]
    [InverseProperty("Comments")]
    public virtual Blog Blog { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Comments")]
    public virtual User User { get; set; } = null!;
}
