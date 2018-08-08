using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCore.Identity.ByPermissions;
using Identity_By_Permissions_Example.Extensions;
using Identity_By_Permissions_Example.Helpers;
using Identity_By_Permissions_Example.Models;
using Identity_By_Permissions_Example.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity_By_Permissions_Example.Controllers
{
    [Route("[controller]/[action]")]
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

            UserRolesManageViewModel vm = new UserRolesManageViewModel
            {
                UserId = id,
                Roles = _roleManager.Roles.Select(x => new UserRolesViewModel
                {
                    RoleName = x.Name,
                    IsActive = userRoles.Contains(x.Name)
                }),
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
        public async Task<IActionResult> ClaimsManage(string roleId)
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
        public async Task<IActionResult> UpdateRoleClaims(string roleId, int policyId, bool set)
        {
            //Obtengo el rol
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
                return Json(false);

            //Obtengo la politica con el claim que que hay que actualizar
            var policyItem = _permissionService.GetPermissionById(policyId);
            if (policyItem == null)
                return Json(false);

            //Si tengo que setear el claim
            if (set)
            {
                //Añado el claim al rol
                var res = await _roleManager.AddClaimAsync(role, new Claim(policyItem.PermissionName, policyItem.PermissionName));
                return Json(res.Succeeded);
            }
            //Si tengo que remover el claim
            else
            {
                //Elimino el claim
                var res = await _roleManager.RemoveClaimAsync(role, new Claim(policyItem.PermissionName, policyItem.PermissionName));
                return Json(res.Succeeded);
            }
        }
    }
}