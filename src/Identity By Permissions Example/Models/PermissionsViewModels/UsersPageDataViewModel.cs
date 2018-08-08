using System.Collections.Generic;

namespace Identity_By_Permissions_Example.Models.PermissionsViewModels
{
    public class UsersPageDataViewModel
    {
        public int TotalUsers { get; set; }
        public IEnumerable<UserViewModel> Users { get; set; }
    }
}