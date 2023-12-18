using System;
using System.Collections.Generic;

namespace newProject.Entities;

public partial class Address
{
    public long Id { get; set; }

    public bool IsPassive { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public long? CityId { get; set; }

    public long? DistrictId { get; set; }

    public long? StreetId { get; set; }

    public long? NeighbourhoodId { get; set; }

    public long? BuildingId { get; set; }

    public long? DoorId { get; set; }

    public virtual ICollection<AddressServiceType> AddressServiceTypes { get; set; } = new List<AddressServiceType>();

    public virtual Building? Building { get; set; }

    public virtual ICollection<CitizenAddress> CitizenAddresses { get; set; } = new List<CitizenAddress>();

    public virtual City? City { get; set; }

    public virtual District? District { get; set; }

    public virtual Door? Door { get; set; }

    public virtual Neighbourhood? Neighbourhood { get; set; }

    public virtual Street? Street { get; set; }

    public virtual ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();

    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
