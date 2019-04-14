using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class VisitLaxType
    {
        public int Id { get; set; }
        public int VisitId { get; set; }
        public Visit Visit { get; set; }
        public int LaxTypeId { get; set; }
        public LaxCementType LaxType { get; set; }
        public int Percent { get; set; }
    }
}