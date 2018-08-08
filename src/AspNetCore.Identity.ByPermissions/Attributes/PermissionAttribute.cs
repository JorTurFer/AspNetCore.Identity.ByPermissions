using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCore.Identity.ByPermissions
{
    /// <summary>
    /// Attribute for trigger the auto Permission detect
    /// </summary>
    public class PermissionAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Creates a permission
        /// </summary>
        /// <param name="Permission">Permision name</param>
        /// <param name="Description">Description to show</param>
        public PermissionAttribute(string Permission, string Description) : base(Permission)
        {
            this.Description = Description;
        }
        /// <summary>
        /// Description of the permission
        /// </summary>
        public string Description { get; }
    }
}
