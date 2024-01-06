using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace newProject.Entities;


public partial class Blog
{
    [Key]
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
