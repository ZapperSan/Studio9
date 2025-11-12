namespace Studio9.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("users")]
public class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("name")]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    [Column("surname")]
    public string Surname { get; set; } = string.Empty;

    [Required]
    [MaxLength(256)]
    [EmailAddress]
    [Column("email")]
    public string Email { get; set; } = string.Empty;

    [Column("is_active")]
    public bool IsActive { get; set; } = true;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
    
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<UserAccessOverride> AccessOverrides { get; set; } = new List<UserAccessOverride>();
}