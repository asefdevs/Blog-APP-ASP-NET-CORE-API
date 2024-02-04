using System;
using System.Collections.Generic;

namespace newProject.Entities;

public partial class Category
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();
}
