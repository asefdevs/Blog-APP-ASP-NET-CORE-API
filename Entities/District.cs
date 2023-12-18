using System;
using System.Collections.Generic;

namespace newProject.Entities;

public partial class District
{
    public long Id { get; set; }

    public bool IsPassive { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? Name { get; set; }

    public string? Value { get; set; }

    public int? Code { get; set; }

    public long CityId { get; set; }

    public virtual ICollection<AddressService> AddressServices { get; set; } = new List<AddressService>();

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual City City { get; set; } = null!;

    public virtual ICollection<Neighbourhood> Neighbourhoods { get; set; } = new List<Neighbourhood>();
}
