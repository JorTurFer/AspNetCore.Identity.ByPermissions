using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Identity.ByPermissions
{
  public static class PermissionsExtensions
  {
    public static void AddPermissions(this AuthorizationOptions options, IPermissionService service)
    {
      foreach (var policyItem in service.GetPermissions())
      {
        options.AddPolicy(policyItem.PermissionName, policy => policy.RequireClaim(policyItem.PermissionName).Build());
      }
    }
  }
}
