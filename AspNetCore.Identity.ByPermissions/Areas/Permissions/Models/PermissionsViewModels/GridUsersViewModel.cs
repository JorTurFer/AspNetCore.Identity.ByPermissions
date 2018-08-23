using System.Collections.Generic;

namespace AspNetCore.Identity.ByPermissions.Areas.Permissions.Models.PermissionsViewModels
{
    public class GridUsersViewModel
    {
        public string Text { get; set; }
        public string Sort { get; set; }
        public bool Ascending { get; set; }
        public bool InvertAscending { get => !Ascending; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalUsers { get; set; }
        public IEnumerable<UserViewModel> Users { get; set; }
    }
}