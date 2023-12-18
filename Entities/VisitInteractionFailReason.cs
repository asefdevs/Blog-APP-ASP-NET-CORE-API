using System;
using System.Collections.Generic;

namespace newProject.Entities;

public partial class VisitInteractionFailReason
{
    public long Id { get; set; }

    public bool IsPassive { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public long? VisitInteractionId { get; set; }

    public string? Description { get; set; }

    public long? FailReasonTypeId { get; set; }

    public virtual FailReasonType? FailReasonType { get; set; }

    public virtual VisitInteraction? VisitInteraction { get; set; }
}
