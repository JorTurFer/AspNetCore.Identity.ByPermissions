using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AspNetCore.Identity.ByPermissions;
using Identity_By_Permissions_Example.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Identity_By_Permissions_Example.Helpers
{
    public class PermissionsManageHelper
    {
        public static PermissionsManageViewModel GetPermissionsManageViewModel(IList<Claim> claims, IdentityRole role, IPermissionService permissionService)
        {
            List<PermissionItemViewModel> policyItemViewModel = new List<PermissionItemViewModel>();
            foreach (var permission in permissionService.GetPermissions())
            {
                //Tiene la politica la coleccion de claims
                var exist = claims.Any(x => string.Compare(x.Type, permission.PermissionName, true) == 0);
                policyItemViewModel.Add(new PermissionItemViewModel { Id = permission.PermissionId, PolicyName = permission.PermissionName, PolicyDesiption = permission.PermissionDescription, IsActive = exist, PolicyGroup = permission.PermissionGroup });
            }
            return new PermissionsManageViewModel { PolicyItems = policyItemViewModel, RoleId = role.Id, Groups = policyItemViewModel.GroupBy(x => x.PolicyGroup).Select(x => x.Key) };
        }
    }
}