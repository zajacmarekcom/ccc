using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class ForeignLaboratoryInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AnnualAmount { get; set; }
        public bool Barter { get; set; }
    }
}