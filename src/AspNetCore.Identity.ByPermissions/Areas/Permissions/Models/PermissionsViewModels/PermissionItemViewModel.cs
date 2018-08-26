using AspNetCore.Identity.ByPermissions;

namespace AspNetCore.Identity.ByPermissions.Areas.Permissions.Models.PermissionsViewModels
{
    public class PermissionItemViewModel : PermissionItem
    {
        public bool IsActive { get; set; }

    }
}