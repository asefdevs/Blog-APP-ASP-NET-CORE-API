using System;
using System.Collections.Generic;

namespace newProject.Entities;

public partial class CitizenAddress
{
    public long Id { get; set; }

    public bool IsPassive { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public long? CitizenId { get; set; }

    public long? AddressId { get; set; }

    public virtual Address? Address { get; set; }

    public virtual Citizen? Citizen { get; set; }
}
