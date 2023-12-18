using System;
using System.Collections.Generic;

namespace newProject.Entities;

public partial class VisitInteractionDelivery
{
    public long Id { get; set; }

    public bool IsPassive { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public long VisitInteractionId { get; set; }

    public long VisitInteractionSupportPackageId { get; set; }

    public bool? IsDelivered { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public virtual VisitInteraction VisitInteraction { get; set; } = null!;

    public virtual VisitInteractionSupportPackage VisitInteractionSupportPackage { get; set; } = null!;

    public virtual ICollection<VisitInteractionSupportPackage> VisitInteractionSupportPackages { get; set; } = new List<VisitInteractionSupportPackage>();
}
