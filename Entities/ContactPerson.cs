using System;
using System.Collections.Generic;

namespace newProject.Entities;

public partial class ContactPerson
{
    public long Id { get; set; }

    public bool IsPassive { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? Name { get; set; }

    public string? Tckn { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Note { get; set; }

    public virtual ICollection<VisitInteraction> VisitInteractions { get; set; } = new List<VisitInteraction>();

    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
