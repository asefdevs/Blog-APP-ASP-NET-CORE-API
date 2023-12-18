using System;
using System.Collections.Generic;

namespace newProject.Entities;

public partial class CitizenService
{
    public long Id { get; set; }

    public bool IsPassive { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public long? CitizenId { get; set; }

    public long? ServiceTypeId { get; set; }

    public string? ServiceName { get; set; }

    public string? Description { get; set; }

    public bool? IsSuccess { get; set; }

    public virtual Citizen? Citizen { get; set; }

    public virtual ServiceType? ServiceType { get; set; }
}
