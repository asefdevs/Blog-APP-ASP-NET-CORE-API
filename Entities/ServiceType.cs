using System;
using System.Collections.Generic;

namespace newProject.Entities;

public partial class ServiceType
{
    public long Id { get; set; }

    public bool IsPassive { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? Name { get; set; }

    public string? Value { get; set; }

    public virtual ICollection<AddressServiceType> AddressServiceTypes { get; set; } = new List<AddressServiceType>();

    public virtual ICollection<CitizenService> CitizenServices { get; set; } = new List<CitizenService>();
}
