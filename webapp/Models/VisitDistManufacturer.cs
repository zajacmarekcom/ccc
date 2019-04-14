using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class VisitDistManufacturer
    {
        public int Id { get; set; }
        public int ManufacturerId { get; set; }
        public int Percent { get; set; }
    }
}