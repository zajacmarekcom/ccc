using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class VisitMarketSegment
    {
        public int Id { get; set; }
        public int VisitId { get; set; }
        public Visit Visit { get; set; }
        public int MarketSegmentId { get; set; }
        public MarketSegment MarketSegment { get; set; }
        public int Percent { get; set; }
    }
}