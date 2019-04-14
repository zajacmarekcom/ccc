using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class VisitDistributor
    {
        public int Id { get; set; }
        public int DistributorId { get; set; }
        public int? MarketAddressId { get; set; }
        [ForeignKey("MarketAddressId")]
        public virtual MarketAddress MarketAddress { get; set; }
        public int Percent { get; set; }
        public virtual ICollection<VisitDistManufacturer> Manufacturers { get; set; }
        public virtual ICollection<VisitDistManufacturersGroup> ManufacturersGroups { get; set; }
    }
}