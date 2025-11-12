namespace Studio9.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("user_access_overrides")]
public class UserAccessOverride
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("access_action_id")]
    public int AccessActionId { get; set; }

    [Column("permission_state")]
    public PermissionState PermissionState { get; set; } = PermissionState.Allowed;

    [MaxLength(500)]
    [Column("reason")]
    public string? Reason { get; set; }

    [Column("granted_at")]
    public DateTime GrantedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    [ForeignKey(nameof(AccessActionId))]
    public AccessAction AccessAction { get; set; } = null!;
}