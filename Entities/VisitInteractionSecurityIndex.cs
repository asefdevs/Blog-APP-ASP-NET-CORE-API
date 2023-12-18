using System;
using System.Collections.Generic;

namespace newProject.Entities;

public partial class VisitInteractionSecurityIndex
{
    public long Id { get; set; }

    public bool IsPassive { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public long? VisitInteractionId { get; set; }

    public string? Description { get; set; }

    public bool? IsSuccess { get; set; }

    public long? SecurityIndexTypeId { get; set; }

    public virtual SecurityIndexType? SecurityIndexType { get; set; }

    public virtual VisitInteraction? VisitInteraction { get; set; }
}
