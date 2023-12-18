using System;
using System.Collections.Generic;

namespace newProject.Entities;

public partial class VisitInteractionSupportPackage
{
    public long Id { get; set; }

    public bool IsPassive { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public long? VisitInteractionId { get; set; }

    public long? SupportPackageId { get; set; }

    public bool? Pending { get; set; }

    public long? VisitInteractionDeliveryId { get; set; }

    public virtual SupportPackage? SupportPackage { get; set; }

    public virtual VisitInteraction? VisitInteraction { get; set; }

    public virtual ICollection<VisitInteractionDelivery> VisitInteractionDeliveries { get; set; } = new List<VisitInteractionDelivery>();

    public virtual VisitInteractionDelivery? VisitInteractionDelivery { get; set; }
}
