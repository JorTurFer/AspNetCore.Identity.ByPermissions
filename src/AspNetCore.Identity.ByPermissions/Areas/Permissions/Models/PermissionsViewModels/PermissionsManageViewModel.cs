using System.Collections.Generic;

namespace AspNetCore.Identity.ByPermissions.Areas.Permissions.Models.PermissionsViewModels
{
    public class PermissionsManageViewModel
    {
        public IEnumerable<PermissionItemViewModel> Permissions { get; set; }
        public string RoleId { get; set; }
        public IEnumerable<int> Groups { get; set; }
    }
}