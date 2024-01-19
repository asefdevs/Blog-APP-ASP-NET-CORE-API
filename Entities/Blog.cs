using System;
using System.Collections.Generic;

namespace newProject.Entities;

public partial class Blog
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int UserID { get; set; }

    public virtual User User { get; set; } = null!;
}
