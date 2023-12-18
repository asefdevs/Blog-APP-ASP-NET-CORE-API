using System;
using System.Collections.Generic;

namespace newProject.Entities;

public partial class UserRequestCount
{
    public long Id { get; set; }

    public bool IsPassive { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public long? UserId { get; set; }

    public string? Method { get; set; }

    public DateTime? CountDate { get; set; }

    public int? Count { get; set; }

    public virtual User? User { get; set; }
}
