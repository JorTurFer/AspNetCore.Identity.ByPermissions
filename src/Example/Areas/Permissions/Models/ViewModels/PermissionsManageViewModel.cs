using System.Collections.Generic;

namespace Example.Areas.Permissions.Models.ViewModels
{
  public class PermissionsManageViewModel
  {
    public IEnumerable<PermissionsItemViewModel> PermissionItems { get; set; }
    public string RoleId { get; set; }
    public IEnumerable<int> Groups { get; set; }
  }
}