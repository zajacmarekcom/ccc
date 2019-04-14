using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class Branch
    {
        public Branch()
        {
            Used = false;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string PostCode { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool Trades { get; set; }
        public string Comments { get; set; }
        public int? UserId { get; set; }
        public bool Used { get; set; }
    }
}