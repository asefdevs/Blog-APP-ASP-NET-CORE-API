using System;
using System.Collections.Generic;

namespace newProject.Entities;

public partial class Image
{
    public int Id { get; set; }

    public int BlogId { get; set; }

    public string ImageName { get; set; } = null!;

    public string ImagePath { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Blog Blog { get; set; } = null!;
}
