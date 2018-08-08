using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Identity.ByPermissions
{
  public class PermissionService : IPermissionService
  {
    //Collection of Permissions in the domain
    static readonly IList<PermissionItem> _permissions = null;

    static PermissionService()
    {
      _permissions = new List<PermissionItem>();
      //Get the assembly
      Assembly asm = Assembly.GetExecutingAssembly();

      //Get the controllers
      var controllers = asm.GetTypes()
      .Where(type => typeof(Controller).IsAssignableFrom(type)); //filter controllers

      int nGroup = 0;
      foreach (var controller in controllers)
      {
        //Get the Permission attribute (if the controller has got it)
        var controllerPermission = controller.GetCustomAttribute<PermissionAttribute>();
        //If the controller has PermissionAttribute, we register it
        if (controllerPermission != null)
          _permissions.Add(new PermissionItem { PermissionId = _permissions.Count, PermissionName = controllerPermission.Policy, PermissionDesiption = controllerPermission.Description, PermissionGroup = nGroup });
        //Same for the methods
        var methodPermissions = controller.GetMethods()
        .Where(type => type.CustomAttributes.Any(x => x.AttributeType == (typeof(PermissionAttribute))))
        .Select(x => x.GetCustomAttribute<PermissionAttribute>()).Distinct();
        foreach (var methodPermission in methodPermissions)
        {
          _permissions.Add(new PermissionItem { PermissionId = _permissions.Count, PermissionName = methodPermission.Policy, PermissionDesiption = methodPermission.Description, PermissionGroup = nGroup });
        }
        nGroup++;
      }
    }

    public IEnumerable<PermissionItem> GetPermissions()
    {
      return _permissions;
    }

    public PermissionItem GetPermissionById(int Id)
    {
      return _permissions.Where(x => x.PermissionId == Id).FirstOrDefault();
    }
  }
}
