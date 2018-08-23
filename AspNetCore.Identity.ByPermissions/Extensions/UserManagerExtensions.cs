using Identity_By_Permissions_Example.Models;
using Identity_By_Permissions_Example.Models.PermissionsViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Identity.ByPermissions.Extensions
{
    public static class UserManagerExtensions
    {
        /// <summary>
        /// Extension for get the page of users
        /// </summary>
        /// <param name="_manager"></param>
        /// <param name="text">Search Input</param>
        /// <param name="page">Current Page</param>
        /// <param name="pageSize">Results per Page</param>
        /// <param name="sort">Sort By:</param>
        /// <param name="ascending">Sort ascending or descending</param>
        /// <returns></returns>
        public static UsersPageDataViewModel GetUserPageAsync(this UserManager<ApplicationUser> _manager, string text, int page, int pageSize, string sort, bool @ascending)
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
