# AspNetCore.Identity.ByPermissions

## Description
This system provides a abstraction layer of the claims system, registering the claims as attributes.The Identity framework manages the access checks and the system store the different permissions in a collection in order to access them in a manager page. This does easier the work of manage the access becouse once the manage zone is written, it's only necesary add attributes.

## In Work:
In the RazorPage branch I'm doing the views code to do full usage package. The branch will be merged when it's done. Between that, it's posible to use following the example proyect.

## Information in build
Soon I am going to write the documentation, sorry (The next info is not complete)...

1. Register the "Permissions Service"
```cSharp
public void ConfigureServices(IServiceCollection services)
{
  ....
  services.AddAuthorization(options =>
  {
      options.AddPermissions(new PermissionService());
  });
  ....
}
```
2. Add the Permissions in the "Controllers" and "Actions"
```cSharp
//[AttributePermission("Permission Name","Description")]
[Permission("Home",  "Can access to Home Controller")]
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
}
```

## Compilation

|Branch|AppVeyor|Travis|
|------|--------|------|
|master|[![Build status](https://ci.appveyor.com/api/projects/status/ujcjv8e7lk3huq4x/branch/master?svg=true)](https://ci.appveyor.com/project/kabestrus/aspnetcore-identity-bypermissions/branch/master)|[![Build Status](https://travis-ci.org/JorTurFer/AspNetCore.Identity.ByPermissions.svg?branch=master)](https://travis-ci.org/JorTurFer/AspNetCore.Identity.ByPermissions)|
|RazorPage|[![Build status](https://ci.appveyor.com/api/projects/status/ujcjv8e7lk3huq4x/branch/RazorPage?svg=true)](https://ci.appveyor.com/project/kabestrus/aspnetcore-identity-bypermissions/branch/RazorPage)|[![Build Status](https://travis-ci.org/JorTurFer/AspNetCore.Identity.ByPermissions.svg?branch=RazorPage)](https://travis-ci.org/JorTurFer/AspNetCore.Identity.ByPermissions)


## Get it from NuGet
[![NuGet][main-nuget-badge]][main-nuget]
[![NuGet][main-nuget-download]][main-nuget]

[main-nuget]: https://www.nuget.org/packages/AspNetCore.Identity.ByPermissions/
[main-nuget-badge]: https://img.shields.io/nuget/v/AspNetCore.Identity.ByPermissions.svg
[main-nuget-download]: https://img.shields.io/nuget/dt/AspNetCore.Identity.ByPermissions.svg
