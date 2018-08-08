using System.Collections.Generic;
using Example.Areas.Permissions.Models.ViewModels;

namespace Example.Areas.Permissions.Models.ViewModels
{
  public class UsersPageDataViewModel
  {
    public int TotalUsers { get; set; }
    public IEnumerable<UserViewModel> Users { get; set; }
  }
}