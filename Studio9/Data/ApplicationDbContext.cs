using Studio9.Models;
using Action = Studio9.Models.Action;

namespace Studio9.Data;

using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Access> Accesses { get; set; }
    public DbSet<Action> Actions { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<AccessAction> AccessActions { get; set; }
    public DbSet<RoleAccessPermission> RoleAccessPermissions { get; set; }
    public DbSet<UserAccessOverride> UserAccessOverrides { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User configurations
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(u => u.Email).IsUnique();
        });

        // UserRole configurations
        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasIndex(ur => new { ur.UserId, ur.RoleId }).IsUnique();
        });

        // AccessAction configurations
        modelBuilder.Entity<AccessAction>(entity =>
        {
            entity.HasIndex(aa => new { aa.AccessId, aa.ActionId }).IsUnique();
        });

        // RoleAccessPermission configurations
        modelBuilder.Entity<RoleAccessPermission>(entity =>
        {
            entity.HasIndex(rap => new { rap.RoleId, rap.AccessActionId }).IsUnique();
        });

        // UserAccessOverride configurations
        modelBuilder.Entity<UserAccessOverride>(entity =>
        {
            entity.HasIndex(uao => new { uao.UserId, uao.AccessActionId }).IsUnique();
        });

        // Enum to string conversion for PermissionState
        modelBuilder.Entity<RoleAccessPermission>()
            .Property(p => p.PermissionState)
            .HasConversion<string>();

        modelBuilder.Entity<UserAccessOverride>()
            .Property(p => p.PermissionState)
            .HasConversion<string>();
    }
}