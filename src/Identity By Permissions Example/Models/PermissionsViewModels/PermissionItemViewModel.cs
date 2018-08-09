using AspNetStandard.Identity.ByPermissions;

namespace Identity_By_Permissions_Example.Models.PermissionsViewModels
{
    public class PermissionItemViewModel : PermissionItem
    {
        public bool IsActive { get; set; }

    }
}