using System;
using System.Collections.Generic;

namespace newProject.Entities;

public partial class Parameter
{
    public long Id { get; set; }

    public bool IsPassive { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? PType { get; set; }

    public string? PKey { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? Value { get; set; }

    public string? ExtraValue1 { get; set; }

    public string? ExtraValue2 { get; set; }

    public string? ExtraValue3 { get; set; }
}
