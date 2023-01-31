using CLUB.Areas.MasterData.Models.Lang;
using CLUB.Data.Entity.Master;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CLUB.Areas.MasterData.Models
{
    public class MembershipViewMdel
    {
        public int membershipId { get; set; }
        [Required]
        public string membershipName { get; set; }
        public string membershipNameBn { get; set; }

        public string membershipShortName { get; set; }

        public MembershipLn fLang { get; set; }

        public IEnumerable<Membership> memberships { get; set; }
    }
}
