namespace Studio9.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("role_access_permissions")]
public class RoleAccessPermission
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("role_id")]
    public int RoleId { get; set; }

    [Column("access_action_id")]
    public int AccessActionId { get; set; }

    [Column("permission_state")]
    public PermissionState PermissionState { get; set; } = PermissionState.Undefined;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    [ForeignKey(nameof(RoleId))]
    public Role Role { get; set; } = null!;

    [ForeignKey(nameof(AccessActionId))]
    public AccessAction AccessAction { get; set; } = null!;
}