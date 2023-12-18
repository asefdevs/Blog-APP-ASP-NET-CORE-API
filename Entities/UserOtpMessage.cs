using System;
using System.Collections.Generic;

namespace newProject.Entities;

public partial class UserOtpMessage
{
    public long Id { get; set; }

    public bool IsPassive { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public long UserId { get; set; }

    public string? MessageId { get; set; }

    public string? OtpCode { get; set; }

    public int TryCount { get; set; }

    public DateTime MessageTime { get; set; }

    public bool Used { get; set; }

    public virtual User User { get; set; } = null!;
}
