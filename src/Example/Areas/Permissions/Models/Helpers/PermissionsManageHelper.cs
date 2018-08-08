using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AspNetCore.Identity.ByPermissions;
using Example.Areas.Permissions.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Example.Areas.Permissions.Models.Helpers
{
  internal class PermissionsManageHelper
  {
    internal static PermissionsManageViewModel GetPermissionsManageViewModel(IList<Claim> claims, IdentityRole role, IPermissionService permissionService)
    {
      List<PermissionsItemViewModel> permissionsItemViewModel = new List<PermissionsItemViewModel>();
      foreach (var permission in permissionService.GetPermissions())
      {
        var exist = claims.Any(x => string.Compare(x.Type, permission.PermissionName, true) == 0);
        permissionsItemViewModel.Add(new PermissionsItemViewModel { PermissionId = permission.PermissionId, PermissionName = permission.PermissionName, PermissionDescription = permission.PermissionDescription, IsActive = exist, PermissionGroup = permission.PermissionGroup });
      }
      return new PermissionsManageViewModel { PermissionItems = permissionsItemViewModel, RoleId = role.Id, Groups = permissionsItemViewModel.GroupBy(x => x.PermissionGroup).Select(x => x.Key) };

    }
  }
}