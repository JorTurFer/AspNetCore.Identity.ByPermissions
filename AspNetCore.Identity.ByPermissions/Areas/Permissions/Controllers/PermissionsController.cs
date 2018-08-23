using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCore.Identity.ByPermissions.Areas.Permissions.Models;
using AspNetCore.Identity.ByPermissions.Areas.Permissions.Models.PermissionsViewModels;
using AspNetCore.Identity.ByPermissions.Extensions;
using AspNetCore.Identity.ByPermissions.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Identity.ByPermissions.Areas.Permissions.Controllers
{
    [Area("Permissions")]
    [Route("[controller]/[action]")]
    [Authorize]
    public class PermissionsController : Controller
    {
        readonly RoleManager<IdentityRole> _roleManager;
        readonly IPermissionService _permissionService;
        readonly UserManager<ApplicationUser> _userManager;

        public PermissionsController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IPermissionService permissionService)
        {
            _roleManager = roleManager;
            _permissionService = permissionService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ManageUsers()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetUsersGrid(GridUsersViewModel vm)
        {
            var pageData = _userManager.GetUserPageAsync(vm.Text, vm.Page, vm.PageSize, vm.Sort, vm.Ascending);
            vm.TotalUsers = pageData.TotalUsers;
            vm.Users = pageData.Users;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            await _userManager.DeleteAsync(user);
            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserRolesManage(string id, GridUsersViewModel vmPrevious)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);

            //TODO check the previous error with the old system
            List<UserRolesViewModel> rolesView = new List<UserRolesViewModel>();
            foreach(var role in _roleManager.Roles)
            {
                var userRole = new UserRolesViewModel
                {
                    RoleName = role.Name,
                    IsActive = userRoles.Contains(role.Name)
                };
                rolesView.Add(userRole);
            }                            
            UserRolesManageViewModel vm = new UserRolesManageViewModel
            {
                UserId = id,
                Roles = rolesView,
                VmPrevious = vmPrevious
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUserRole(string id, string roleName, bool set)
        {
            //Busco el usuario
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            //En funcione del check, lo añado o lo remuevo
            if (set)
                await _userManager.AddToRoleAsync(user, roleName);
            else
                await _userManager.RemoveFromRoleAsync(user, roleName);

            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ManageRoles()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewRole(string roleName)
        {
            //Compruebo que no exista
            var exists = _roleManager.Roles.Any(x => string.Compare(x.Name, roleName, false) == 0);
            if (exists)
                return Json(false);
            //Si no existe lo creo
            IdentityRole role = new IdentityRole();
            role.Name = roleName;
            await _roleManager.CreateAsync(role);
            return Json(role.Id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
                return Json(false);
            //Si no existe lo creo
            var res = await _roleManager.DeleteAsync(role);
            return Json(res.Succeeded);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PermissionsManage(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
                return Json(false);
            var claims = await _roleManager.GetClaimsAsync(role);
            PermissionsManageViewModel model = PermissionsManageHelper.GetPermissionsManageViewModel(claims, role, _permissionService);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRolePermissions(string roleId, int policyId, bool set)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
                return Json(false);

            var policyItem = _permissionService.GetPermissionById(policyId);
            if (policyItem == null)
                return Json(false);

            if (set)
            {
                var res = await _roleManager.AddClaimAsync(role, new Claim(policyItem.PermissionName, policyItem.PermissionName));
                return Json(res.Succeeded);
            }
            else
            {
                var res = await _roleManager.RemoveClaimAsync(role, new Claim(policyItem.PermissionName, policyItem.PermissionName));
                return Json(res.Succeeded);
            }
        }
    }
}