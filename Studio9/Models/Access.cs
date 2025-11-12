namespace Studio9.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("access")]
public class Access
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("title")]
    public string Title { get; set; } = string.Empty;
    
    [MaxLength(300)]
    [Column("note")]
    public string Note { get; set; } = string.Empty;
    
    public ICollection<AccessAction> AccessActions { get; set; } = new List<AccessAction>();
    public ICollection<RoleAccessPermission> RoleAccessPermissions { get; set; } = new List<RoleAccessPermission>();
    public ICollection<UserAccessOverride> UserAccessOverrides { get; set; } = new List<UserAccessOverride>();

}