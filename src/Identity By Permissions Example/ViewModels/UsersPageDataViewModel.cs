using System.Collections.Generic;

namespace Identity_By_Permissions_Example.ViewModels
{
    public class UsersPageDataViewModel
    {
        public int TotalUsers { get; set; }
        public IEnumerable<UserViewModel> Users { get; set; }
    }
}