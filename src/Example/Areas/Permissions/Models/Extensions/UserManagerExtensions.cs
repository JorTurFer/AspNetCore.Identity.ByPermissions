using Example.Areas.Permissions.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.Areas.Permissions.Models.Extensions
{
  public static class UserManagerExtensions
  {
    public static UsersPageDataViewModel GetUserPageAsync(this UserManager<IdentityUser> _manager, string text, int page, int pageSize, string sort, bool @ascending)
    {
      var usersQuery = _manager.Users;
      switch (sort.ToLower())
      {
        case "email":
          usersQuery = ascending
              ? usersQuery.OrderBy(p => p.Email)
              : usersQuery.OrderByDescending(p => p.Email);
          break;
        default:
          usersQuery = ascending
              ? usersQuery.OrderBy(p => p.UserName)
              : usersQuery.OrderByDescending(p => p.UserName);
          break;
      }

      if (!string.IsNullOrWhiteSpace(text))
        usersQuery = usersQuery.Where(u => u.UserName.Contains(text) || u.Email.Contains(text) || u.PhoneNumber.Contains(text));

      var count = usersQuery.Count();

      var data = usersQuery.Skip((page - 1) * pageSize).Take(pageSize).Select(x => new UserViewModel
      {
        UserName = x.UserName,
        Email = x.Email,
        Id = x.Id
      }).ToList();
      var result = new UsersPageDataViewModel
      {
        TotalUsers = count,
        Users = data,
      };
      return result;
    }
  }
}
