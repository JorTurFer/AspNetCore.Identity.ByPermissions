
using AspNetCore.Identity.ByPermissions;

namespace Example.Areas.Permissions.Models.ViewModels
{
  internal class PermissionsItemViewModel : PermissionItem
  {
    public bool IsActive { get; set; }
  }
}