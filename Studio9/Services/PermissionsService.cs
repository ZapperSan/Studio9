using Studio9.Data;
using Studio9.Models;

namespace Studio9.Services;

using Microsoft.EntityFrameworkCore;


public class PermissionService
{
    private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

    public PermissionService(IDbContextFactory<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<bool> HasPermissionAsync(int userId, string accessName, string actionName)
    {
        using var _context = _contextFactory.CreateDbContext();

        var accessAction = await _context.AccessActions
            .Include(aa => aa.Access)
            .Include(aa => aa.Action)
            .FirstOrDefaultAsync(aa =>
                aa.Access.Title == accessName &&
                aa.Action.Title == actionName);

        if (accessAction == null)
            return false;

        var userOverride = await _context.UserAccessOverrides
            .FirstOrDefaultAsync(uao =>
                uao.UserId == userId &&
                uao.AccessActionId == accessAction.Id);

        if (userOverride != null)
            return userOverride.PermissionState == PermissionState.Allowed;

        var rolePermissions = await _context.UserRoles
            .Where(ur => ur.UserId == userId)
            .Join(_context.RoleAccessPermissions,
                ur => ur.RoleId,
                rap => rap.RoleId,
                (ur, rap) => rap)
            .Where(rap => rap.AccessActionId == accessAction.Id)
            .Select(rap => rap.PermissionState)
            .ToListAsync();

        if (rolePermissions.Any())
        {
            if (rolePermissions.Contains(PermissionState.Denied))
                return false;
            if (rolePermissions.Contains(PermissionState.Allowed))
                return true;
        }

        return false;
    }

    public async Task<Dictionary<string, Dictionary<string, bool>>> GetUserPermissionsAsync(int userId)
    {
        using var _context = _contextFactory.CreateDbContext();

        var result = new Dictionary<string, Dictionary<string, bool>>();

        var allAccessActions = await _context.AccessActions
            .Include(aa => aa.Access)
            .Include(aa => aa.Action)
            .ToListAsync();

        foreach (var accessAction in allAccessActions)
        {
            var hasPermission = await HasPermissionAsync(
                userId,
                accessAction.Access.Title,
                accessAction.Action.Title
            );

            if (!result.ContainsKey(accessAction.Access.Title))
                result[accessAction.Access.Title] = new();

            result[accessAction.Access.Title][accessAction.Action.Title] = hasPermission;
        }

        return result;
    }
}