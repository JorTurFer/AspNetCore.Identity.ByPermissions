﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Identity_By_Permissions_Example.Models;
using AspNetStandard.Identity.ByPermissions;

namespace Identity_By_Permissions_Example.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //This line add a Permissions named "About" with a description and assigned to the action
        [Permission("About",  "Can see about page")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        //This line add a Permissions named "Contact" with a description and assigned to the action
        [Permission("Contact", "Can see contact page")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
