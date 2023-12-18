using System;
using System.Collections.Generic;

namespace newProject.Entities;

public partial class VisitInteractionFeedback
{
    public long Id { get; set; }

    public bool IsPassive { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public long? VisitInteractionId { get; set; }

    public string? Description { get; set; }

    public long? FeedbackTopicId { get; set; }

    public long? FeedbackSubTopicId { get; set; }

    public bool? IsSuccess { get; set; }

    public virtual FeedbackSubTopic? FeedbackSubTopic { get; set; }

    public virtual FeedbackTopic? FeedbackTopic { get; set; }

    public virtual VisitInteraction? VisitInteraction { get; set; }
}
