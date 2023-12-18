using System;
using System.Collections.Generic;

namespace newProject.Entities;

public partial class Citizen
{
    public long Id { get; set; }

    public bool IsPassive { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? Tckn { get; set; }

    public string? Name { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? Gender { get; set; }

    public virtual ICollection<CitizenAddress> CitizenAddresses { get; set; } = new List<CitizenAddress>();

    public virtual ICollection<CitizenService> CitizenServices { get; set; } = new List<CitizenService>();
}
