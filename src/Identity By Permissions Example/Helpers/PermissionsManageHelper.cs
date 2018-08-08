using System;
using System.Collections.Generic;
using System.Security.Claims;
using AspNetCore.Identity.ByPermissions;
using Microsoft.AspNetCore.Identity;

namespace Identity_By_Permissions_Example.Helpers
{
    public class PermissionsManageHelper
    {
        public static ClaimsManageViewModel GetClaimsManageViewModel(IList<Claim> claims, IdentityRole role, IPoliciesManager policiesManager)
        {
            List<PermissionItemViewModel> policyItemViewModel = new List<PermissionItemViewModel>();
            foreach (var policy in policiesManager.GetPolicies())
            {
                //Tiene la politica la coleccion de claims
                var exist = claims.Any(x => string.Compare(x.Type, policy.PolicyName, true) == 0);
                policyItemViewModel.Add(new PolicyItemViewModel { Id = policy.Id, PolicyName = policy.PolicyName, PolicyDesiption = policy.PolicyDesiption, IsActive = exist, PolicyGroup = policy.PolicyGroup });
            }
            return new ClaimsManageViewModel { PolicyItems = policyItemViewModel, RoleId = role.Id, Groups = policyItemViewModel.GroupBy(x => x.PolicyGroup).Select(x => x.Key) };
        }
    }
}