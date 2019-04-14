using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class VisitManufacturers
    {
        public int Id { get; set; }
        public int ManufacturerId { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public int Percent { get; set; }
    }
}