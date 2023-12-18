using System;
using System.Collections.Generic;

namespace newProject.Entities;

public partial class AuthorizationType
{
    public long Id { get; set; }

    public bool? IsPassive { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
