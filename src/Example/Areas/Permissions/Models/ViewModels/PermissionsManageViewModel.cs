using System.Collections.Generic;
using Example.Areas.Permissions.Models.ViewModels;

namespace Example.Areas.Permissions.Models.Helpers
{
  internal class PermissionsManageViewModel
  {
    public IEnumerable<PermissionsItemViewModel> PermissionItems { get; set; }
    public string RoleId { get; set; }
    public IEnumerable<int> Groups { get; set; }
  }
}