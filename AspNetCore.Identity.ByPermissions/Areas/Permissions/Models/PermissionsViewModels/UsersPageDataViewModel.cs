using System.Collections.Generic;

namespace AspNetCore.Identity.ByPermissions.Areas.Permissions.Models.PermissionsViewModels
{
    public class UsersPageDataViewModel
    {
        public int TotalUsers { get; set; }
        public IEnumerable<UserViewModel> Users { get; set; }
    }
}