using System.Collections.Generic;

namespace Identity_By_Permissions_Example.Models.PermissionsViewModels
{
    public class PermissionsManageViewModel
    {
        public IEnumerable<PermissionItemViewModel> Permissions { get; set; }
        public string RoleId { get; set; }
        public IEnumerable<int> Groups { get; set; }
    }
}