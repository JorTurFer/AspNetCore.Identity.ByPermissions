using Microsoft.AspNetCore.Authorization;

namespace AspNetCore.Identity.ByPermissions
{
    public static class PermissionsExtensions
    {
        /// <summary>
        /// Add the attributed permissions to the registry
        /// </summary>
        /// <param name="options"></param>
        /// <param name="service">An instance of the Permission Service</param>
        public static void AddPermissions(this AuthorizationOptions options, IPermissionService service)
        {
            foreach (var policyItem in service.GetPermissions())
            {
                options.AddPolicy(policyItem.PermissionName, policy => policy.RequireClaim(policyItem.PermissionName).Build());
            }
        }
    }
}
