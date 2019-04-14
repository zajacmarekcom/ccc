using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class VisitPackedType
    {
        public int Id { get; set; }
        public int VisitId { get; set; }
        public Visit Visit { get; set; }
        public int PackedTypeId { get; set; }
        public PackedCementType PackedType { get; set; }
        public int Percent { get; set; }
    }
}