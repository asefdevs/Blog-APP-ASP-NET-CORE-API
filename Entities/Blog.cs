using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace newProject.Entities;

public partial class Blog
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Blogs")]
    public virtual Category Category { get; set; } = null!;

    [InverseProperty("Blog")]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [InverseProperty("Blog")]
    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    [ForeignKey("UserId")]
    [InverseProperty("Blogs")]
    public virtual User User { get; set; } = null!;
}
