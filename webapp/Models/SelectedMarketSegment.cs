using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    [NotMapped]
    public class SelectedMarketSegment
    {
        public int Id { get; set; }
        public int EId { get; set; }
        public bool Checked { get; set; }
        public int Percent { get; set; }
        public string Name { get; set; }
    }
}