using System;
using System.Collections.Generic;

namespace newProject.Entities;

public partial class VisitInteraction
{
    public long Id { get; set; }

    public bool IsPassive { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public long? VisitId { get; set; }

    public long? VisitInteractionTypeId { get; set; }

    public long? ContactPersonId { get; set; }

    public virtual ContactPerson? ContactPerson { get; set; }

    public virtual Visit? Visit { get; set; }

    public virtual ICollection<VisitInteractionDelivery> VisitInteractionDeliveries { get; set; } = new List<VisitInteractionDelivery>();

    public virtual ICollection<VisitInteractionFailReason> VisitInteractionFailReasons { get; set; } = new List<VisitInteractionFailReason>();

    public virtual ICollection<VisitInteractionFeedback> VisitInteractionFeedbacks { get; set; } = new List<VisitInteractionFeedback>();

    public virtual ICollection<VisitInteractionSecurityIndex> VisitInteractionSecurityIndices { get; set; } = new List<VisitInteractionSecurityIndex>();

    public virtual ICollection<VisitInteractionSupportPackage> VisitInteractionSupportPackages { get; set; } = new List<VisitInteractionSupportPackage>();

    public virtual VisitInteractionType? VisitInteractionType { get; set; }
}
