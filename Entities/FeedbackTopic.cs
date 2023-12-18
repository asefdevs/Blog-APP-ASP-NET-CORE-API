using System;
using System.Collections.Generic;

namespace newProject.Entities;

public partial class FeedbackTopic
{
    public long Id { get; set; }

    public bool IsPassive { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? Name { get; set; }

    public string? Value { get; set; }

    public virtual ICollection<FeedbackSubTopic> FeedbackSubTopics { get; set; } = new List<FeedbackSubTopic>();

    public virtual ICollection<VisitInteractionFeedback> VisitInteractionFeedbacks { get; set; } = new List<VisitInteractionFeedback>();
}
