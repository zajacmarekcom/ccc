using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class MarketAddress
    {
        public int Id { get; set; }
        [Required]
        public int DistributorId { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string BuildingNumber { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string PostCode { get; set; }
        [Required]
        public int ProvinceId { get; set; }
        [ForeignKey("ProvinceId")]
        public virtual Province Province { get; set; }
        [Required]
        public int DistrictId { get; set; }
        [ForeignKey("DistrictId")]
        public virtual District District { get; set; }
    }
}