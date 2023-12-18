using System;
using System.Collections.Generic;

namespace newProject.Entities;

public partial class AddressServiceType
{
    public long Id { get; set; }

    public bool IsPassive { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public long? ServiceTypeId { get; set; }

    public long? AddressId { get; set; }

    public virtual Address? Address { get; set; }

    public virtual ServiceType? ServiceType { get; set; }
}
