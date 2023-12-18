using System;
using System.Collections.Generic;

namespace newProject.Entities;

public partial class Door
{
    public long Id { get; set; }

    public bool IsPassive { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? Name { get; set; }

    public string? Value { get; set; }

    public int? Code { get; set; }

    public long BuildingId { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual Building Building { get; set; } = null!;
}
