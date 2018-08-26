using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AspNetCore.Identity.ByPermissions
{
    public class PermissionService : IPermissionService
    {
        //Collection of Permissions in the domain
        static readonly IList<PermissionItem> _permissions = null;

        static PermissionService()
        {
            _permissions = new List<PermissionItem>();

            //Get the controllers
            var controllers = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => x.IsSubclassOf(typeof(Controller)));
           
            int nGroup = 0;
            foreach (var controller in controllers)
            {
                //Get the Permission attribute (if the controller has got it)
                var controllerPermission = controller.GetCustomAttribute<PermissionAttribute>();
                //If the controller has PermissionAttribute, we register it
                if (controllerPermission != null)
                    _permissions.Add(new PermissionItem { PermissionId = _permissions.Count, PermissionName = controllerPermission.Policy, PermissionDescription = controllerPermission.Description, PermissionGroup = nGroup });
                //Same for the methods
                var methodPermissions = controller.GetMethods()
                .Where(type => type.CustomAttributes.Any(x => x.AttributeType == (typeof(PermissionAttribute))))
                .Select(x => x.GetCustomAttribute<PermissionAttribute>()).Distinct();
                foreach (var methodPermission in methodPermissions)
                {
                    _permissions.Add(new PermissionItem { PermissionId = _permissions.Count, PermissionName = methodPermission.Policy, PermissionDescription = methodPermission.Description, PermissionGroup = nGroup });
                }
                nGroup++;
            }
        }
        /// <summary>
        /// Get the collection of permissions
        /// </summary>
        /// <returns>IEnumerable<PermissionItem> Permissions</returns>
        public IEnumerable<PermissionItem> GetPermissions()
        {
            return _permissions;
        }
        /// <summary>
        /// Get a single permission by Id
        /// </summary>
        /// <param name="Id">Permission Id</param>
        /// <returns>PermissionItem</returns>
        public PermissionItem GetPermissionById(int Id)
        {
            return _permissions.Where(x => x.PermissionId == Id).FirstOrDefault();
        }
    }
}
