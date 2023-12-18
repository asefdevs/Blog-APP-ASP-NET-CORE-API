using System;
using System.Collections.Generic;

namespace newProject.Entities;

public partial class Neighbourhood
{
    public long Id { get; set; }

    public bool IsPassive { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? Name { get; set; }

    public string? Value { get; set; }

    public int? Code { get; set; }

    public long DistrictId { get; set; }

    public virtual ICollection<AddressService> AddressServices { get; set; } = new List<AddressService>();

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual District District { get; set; } = null!;

    public virtual ICollection<Street> Streets { get; set; } = new List<Street>();
}
