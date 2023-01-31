using CLUB.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Areas.MasterData.Models
{
    public class ParticipantHeadViewModel
    {
        public int participantHeadId { get; set; }
        public string name { get; set; }

        public IEnumerable<ParticipantHead> participantHeads { get; set; }
    }
}
