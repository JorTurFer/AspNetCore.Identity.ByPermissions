using System.Collections.Generic;

namespace AspNetCore.Identity.ByPermissions.Areas.Permissions.Models.PermissionsViewModels
{
    public class UserRolesManageViewModel
    {
        public string UserId { get; set; }

        public IEnumerable<UserRolesViewModel> Roles { get; set; }

        public GridUsersViewModel VmPrevious { get; set; }
    }
}