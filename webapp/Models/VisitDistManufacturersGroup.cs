using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class VisitDistManufacturersGroup
    {
        public int Id { get; set; }
        public int ManufacturersGroupId { get; set; }
        public bool SelectedManufacturers { get; set; }
        public virtual ICollection<VisitDistManufacturer> Mans { get; set; }
        public int Percent { get; set; }
    }
}