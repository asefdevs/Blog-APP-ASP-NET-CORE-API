using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace newProject.Entities;

public partial class Category
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string? Title { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UpdatedAt { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();
}
