using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class ClientStep1
    {
        public int Id { get; set; }
        public virtual ICollection<VisitMarketSegment> VisitMarketSegments { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }
        public int VisitId { get; set; }
    }
}