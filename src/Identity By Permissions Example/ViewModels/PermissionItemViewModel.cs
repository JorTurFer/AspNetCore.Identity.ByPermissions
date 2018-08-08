using AspNetCore.Identity.ByPermissions;

namespace Identity_By_Permissions_Example.ViewModels
{
    public class PermissionItemViewModel : PermissionItem
    {
        public bool IsActive { get; set; }

    }
}