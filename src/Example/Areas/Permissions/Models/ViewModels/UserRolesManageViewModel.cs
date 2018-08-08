using Example.Areas.Permissions.Models.ViewModels;
using System.Collections.Generic;

namespace Example.Areas.Permissions.ViewModel
{
  public class UserRolesManageViewModel
  {
    public string UserId { get; set; }

    public IEnumerable<UserRolesViewModel> Roles { get; set; }

    public GridUsersViewModel VmPrevious { get; set; }
  }
}