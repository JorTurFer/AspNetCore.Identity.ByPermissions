using System.Collections.Generic;
using Identity_By_Permissions_Example.ViewModels;

namespace Identity_By_Permissions_Example.ViewModels
{
    public class PermissionsManageViewModel
    {
        public IEnumerable<PermissionItemViewModel> Permissions { get; set; }
        public string RoleId { get; set; }
        public IEnumerable<int> Groups { get; set; }
    }
}