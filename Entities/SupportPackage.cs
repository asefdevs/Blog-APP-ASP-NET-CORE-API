using System;
using System.Collections.Generic;

namespace newProject.Entities;

public partial class SupportPackage
{
    public long Id { get; set; }

    public bool IsPassive { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? Name { get; set; }

    public string? Value { get; set; }

    public virtual ICollection<VisitInteractionSupportPackage> VisitInteractionSupportPackages { get; set; } = new List<VisitInteractionSupportPackage>();
}
