using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class ManufacturersGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
    }
}