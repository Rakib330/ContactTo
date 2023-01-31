using CLUB.Areas.Employee.Models.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CLUB.Areas.Employee.Models
{
    public class PublicationViewModel
    {
        public string employeeID { get; set; }

        public string publicationId { get; set; }

        [Required]
        [Display(Name = "Publication Name")]
        public string publicationName { get; set; }

        [Required]
        [Display(Name = "Publication Type")]
        public string publicationType { get; set; }

        [Display(Name = "Publication Place")]
        public string publicationPlace { get; set; }

        [Required]
        [Display(Name = "Publication Date")]
        public DateTime? publicationDate { get; set; }

        public string action { get; set; }

        public string employeeNameCode { get; set; }

        public string publicationNubmer { get; set; }

        public Publication fLang { get; set; }

        public IEnumerable<Data.Entity.Employee.Publication> publications { get; set; }
    }
}
