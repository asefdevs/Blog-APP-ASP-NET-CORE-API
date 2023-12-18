using System;
using System.Collections.Generic;

namespace newProject.Entities;

public partial class AddressService
{
    public long Id { get; set; }

    public bool IsPassive { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? ServiceName { get; set; }

    public string? ServiceSubType { get; set; }

    public string? FullAddress { get; set; }

    public string? Responsible { get; set; }

    public long DistrictId { get; set; }

    public long? NeighbourhoodId { get; set; }

    public virtual District District { get; set; } = null!;

    public virtual Neighbourhood? Neighbourhood { get; set; }
}
