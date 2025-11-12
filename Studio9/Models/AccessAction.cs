namespace Studio9.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Many-To-Many junction table

[Table("access_actions")]
public class AccessAction
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("access_id")]
    public int AccessId { get; set; }

    [Column("action_id")]
    public int ActionId { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey(nameof(AccessId))]
    public Access Access { get; set; } = null!;

    [ForeignKey(nameof(ActionId))]
    public Action Action { get; set; } = null!;

    public ICollection<RoleAccessPermission> RoleAccessPermissions { get; set; } = new List<RoleAccessPermission>();
    public ICollection<UserAccessOverride> UserAccessOverrides { get; set; } = new List<UserAccessOverride>();
}