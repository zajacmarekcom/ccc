using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class SelectedManufacturersGroups
    {
        public int Id { get; set; }
        public bool Checked { get; set; }
        public int Percent { get; set; }
        public bool SelectedManufacturers { get; set; }
        public IEnumerable<SelectedManufacturers> Manufacturers { get; set; }
    }
}