using System;
using System.Collections.Generic;

namespace newProject.Entities;

public partial class User
{
    public long Id { get; set; }

    public bool IsPassive { get; set; }

    public bool IsRecordPassive { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? Tckn { get; set; }

    public string? FullName { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? Email { get; set; }

    public string? Gender { get; set; }

    public long? AuthorizationTypeId { get; set; }

    public string? Password { get; set; }

    public DateTime? CountDate { get; set; }

    public int? Count { get; set; }

    public virtual AuthorizationType? AuthorizationType { get; set; }

    public virtual ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();

    public virtual ICollection<UserOtpMessage> UserOtpMessages { get; set; } = new List<UserOtpMessage>();

    public virtual ICollection<UserRequestCount> UserRequestCounts { get; set; } = new List<UserRequestCount>();

    public virtual ICollection<VisitTeam> VisitTeams { get; set; } = new List<VisitTeam>();

    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
