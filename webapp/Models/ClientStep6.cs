using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class ClientStep6
    {
        public int Id { get; set; }
        public int VisitId { get; set; }
        public int OwnLaboratory { get; set; }
        public int ForeignLaboratory { get; set; }
        public virtual ICollection<ForeignLaboratoryInfo> ForeignLaboratories { get; set; }
    }
}