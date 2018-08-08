using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Identity.ByPermissions
{
    public class PermissionItem
    {
        //Permission Id
        public int PermissionId { get; set; }
        //Name to register
        public string PermissionName { get; set; }
        //Description to show
        public string PermissionDescription { get; set; }
        //Group of the Permission
        public int PermissionGroup { get; set; }
    }
}
