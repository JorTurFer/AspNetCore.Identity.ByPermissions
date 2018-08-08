using System.Collections.Generic;

namespace Identity_By_Permissions_Example.Models.PermissionsViewModels
{
    public class UserRolesManageViewModel
    {
        public string UserId { get; set; }

        public IEnumerable<UserRolesViewModel> Roles { get; set; }

        public GridUsersViewModel VmPrevious { get; set; }
    }
}