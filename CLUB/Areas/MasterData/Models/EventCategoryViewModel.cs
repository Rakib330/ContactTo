using CLUB.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Areas.MasterData.Models
{
    public class EventCategoryViewModel
    {
        public int categoryId { get; set; }

        public string name { get; set; }

        public string remarks { get; set; }

        public IEnumerable<EventCategory> eventCategories { get; set; }
    }
}
