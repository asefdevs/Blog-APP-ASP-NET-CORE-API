using System;
using System.Collections.Generic;

namespace newProject.Entities;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();
}
