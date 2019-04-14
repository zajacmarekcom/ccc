using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class Distributor
    {
        public Distributor()
        {
            MarketAddresses = new List<MarketAddress>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string BuildingNumber { get; set; }
        [Required]
        public string PostCode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string NIP { get; set; }

        public string Lat { get; set; }
        public string Lng { get; set; }

        public bool IsMarket { get; set; }
        public virtual ICollection<MarketAddress> MarketAddresses { get; set; }
    }
}