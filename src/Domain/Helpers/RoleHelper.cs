using Domain.Constants;

namespace Domain.Helpers;

public class RoleHelper
{
    /// <summary>
    ///     Get default tenant roles
    /// </summary>
    /// <returns>HashSet Guid</returns>
    public static readonly HashSet<Guid> DefaultTenantRoles = new()
    {
        UserConstants.Role.SystemAdmin,
        UserConstants.Role.SuperAdmin
    };

    /// <summary>
    ///     Get normal tenant roles
    /// </summary>
    /// <returns>HashSet Guid</returns>
    public static readonly HashSet<Guid> NormalTenantRoles = new()
    {
        UserConstants.Role.TenantAdmin,
        UserConstants.Role.WorkFlowUser,
        UserConstants.Role.WorkFlowAdmin
    };

    /// <summary>
    ///     Get allowed roles for tenant
    /// </summary>
    /// <returns>HashSet Guid</returns>
    public static HashSet<Guid> GetAllowedRoles(Guid tenantId)
    {
        return tenantId == GlobalConstants.Tenant.DefaultTenant
                 ? DefaultTenantRoles
                 : NormalTenantRoles;
    }
}