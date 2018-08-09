using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AspNetStandard.Identity.ByPermissions;
using Identity_By_Permissions_Example.Models.PermissionsViewModels;
using Microsoft.AspNetCore.Identity;

namespace Identity_By_Permissions_Example.Helpers
{
    public class PermissionsManageHelper
    {
        /// <summary>
        /// Generate the Permission manage ViewModel
        /// </summary>
        /// <param name="claims">User Claims</param>
        /// <param name="role">Role to check</param>
        /// <param name="permissionService">Permissions Service</param>
        /// <returns></returns>
        public static PermissionsManageViewModel GetPermissionsManageViewModel(IList<Claim> claims, IdentityRole role, IPermissionService permissionService)
        {
            List<PermissionItemViewModel> policyItemViewModel = new List<PermissionItemViewModel>();
            foreach (var permission in permissionService.GetPermissions())
            {
                //Has the claim collection got the Permission?
                var exist = claims.Any(x => string.Compare(x.Type, permission.PermissionName, true) == 0);
                policyItemViewModel.Add(new PermissionItemViewModel { PermissionId = permission.PermissionId, PermissionName = permission.PermissionName, PermissionDescription = permission.PermissionDescription, IsActive = exist, PermissionGroup = permission.PermissionGroup });
            }
            return new PermissionsManageViewModel { Permissions = policyItemViewModel, RoleId = role.Id, Groups = policyItemViewModel.GroupBy(x => x.PermissionGroup).Select(x => x.Key) };
        }
    }
}