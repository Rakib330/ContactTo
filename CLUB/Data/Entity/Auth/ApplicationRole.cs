using Microsoft.AspNetCore.Identity;

using System.ComponentModel.DataAnnotations;

namespace CLUB.Data.Entity.Auth
{
    public class ApplicationRole:IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string roleName) : base(roleName)
        {
        }
        public string roleNature { get; set; }
    }
}
