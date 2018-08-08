using AspNetCore.Identity.ByPermissions;

namespace Example.Areas.Permissions.Models.ViewModels
{
  public class PermissionsItemViewModel : PermissionItem
  {
    public bool IsActive { get; set; }
  }
}