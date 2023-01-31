using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Areas.Auth.Models
{
    public class RoleViewModel
    {
        [Required]
        public string name { get; set; }

        public List<string> roles { get; set; }
    }
}
