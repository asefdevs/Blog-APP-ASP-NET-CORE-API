using System;
using System.Collections.Generic;

namespace newProject.Entities;

public partial class Visit
{
    public long Id { get; set; }

    public bool IsPassive { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsFinished { get; set; }

    public long? AddressId { get; set; }

    public long? UserId { get; set; }

    public bool? IsFailed { get; set; }

    public long? AuthorizationTypeId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? IsDifferentPerson { get; set; }

    public long? DifferentPersonId { get; set; }

    public virtual Address? Address { get; set; }

    public virtual AuthorizationType? AuthorizationType { get; set; }

    public virtual ContactPerson? DifferentPerson { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<VisitInteraction> VisitInteractions { get; set; } = new List<VisitInteraction>();

    public virtual ICollection<VisitTeam> VisitTeams { get; set; } = new List<VisitTeam>();
}
