using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class ClientStep2
    {
        public int Id { get; set; }
        public virtual Assortment Assortment { get; set; }
        public virtual ICollection<VisitLaxType> LaxTypes { get; set; }
        public virtual ICollection<VisitPackedType> PackedTypes { get; set; }
        public int VisitId { get; set; }
    }
}