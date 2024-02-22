using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace newProject.Entities;

[Table("UserProfile")]
public partial class UserProfile
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    [StringLength(50)]
    public string? FirstName { get; set; }

    [StringLength(50)]
    public string? LastName { get; set; }

    [Column(TypeName = "date")]
    public DateTime? BirthDate { get; set; }

    [StringLength(50)]
    public string? Country { get; set; }

    [StringLength(1000)]
    public string? Bio { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserProfiles")]
    public virtual User User { get; set; } = null!;
}
