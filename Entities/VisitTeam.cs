using System;
using System.Collections.Generic;

namespace newProject.Entities;

public partial class VisitTeam
{
    public long Id { get; set; }

    public bool IsPassive { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsFinished { get; set; }

    public long? VisitId { get; set; }

    public long? UserId { get; set; }

    public virtual User? User { get; set; }

    public virtual Visit? Visit { get; set; }
}
