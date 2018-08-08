using System.Collections.Generic;

namespace Example.Areas.Permissions.Models.ViewModels
{
  public class UsersPageDataViewModel
  {
    public int TotalUsers { get; set; }
    public IEnumerable<UserViewModel> Users { get; set; }
  }
}