using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class SelectedDistributor
    {
        public int Id { get; set; }
        public int? MarketAddressId { get; set; }
        public virtual MarketAddress MarketAddress { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string NIP { get; set; }
        public bool IsMarket { get; set; }
        public IEnumerable<MarketAddress> Addresses { get; set; }
        public IEnumerable<SelectedManufacturers> Manufacturers { get; set; }
        public IEnumerable<SelectedManufacturersGroups> ManufsGroups { get; set; }
        public int Percent { get; set; }
        public bool Checked { get; set; }
    }
}