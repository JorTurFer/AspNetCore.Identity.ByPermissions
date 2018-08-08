using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCore.Identity.ByPermissions
{
  public class AuthorizePermissionAttribute : AuthorizeAttribute
  {
    public AuthorizePermissionAttribute(string Permission,string Description):base(Permission)
    {
      this.Description = Description; 
    }

    public string Description { get;}
  }
}
