using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace newProject.Entities;

public partial class Image
{
    [Key]
    public int Id { get; set; }

    public int BlogId { get; set; }

    [StringLength(255)]
    public string ImageName { get; set; } = null!;

    [StringLength(500)]
    public string ImagePath { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UpdatedAt { get; set; }

    [ForeignKey("BlogId")]
    [InverseProperty("Images")]
    public virtual Blog Blog { get; set; } = null!;
}
