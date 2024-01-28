using System;
using System.Collections.Generic;

namespace newProject.Entities;

public partial class Comment
{
    public int Id { get; set; }

    public int BlogId { get; set; }

    public int UserId { get; set; }

    public string Context { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Blog Blog { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
