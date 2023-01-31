using CLUB.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Areas.MasterData.Models
{
    public class ParticipantTypeViewModel
    {
        public int participantTypeId { get; set; }
        public string name { get; set; }

        public IEnumerable<ParticipantType> participantTypes { get; set; }
    }
}
