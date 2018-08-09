using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetStandard.Identity.ByPermissions
{
    public interface IPermissionService
    {
        /// <summary>
        /// Get the collection of permissions
        /// </summary>
        /// <returns>IEnumerable<PermissionItem> Permissions</returns>
        IEnumerable<PermissionItem> GetPermissions();
        /// <summary>
        /// Get a single permission by Id
        /// </summary>
        /// <param name="Id">Permission Id</param>
        /// <returns>PermissionItem</returns>
        PermissionItem GetPermissionById(int Id);
    }
}
