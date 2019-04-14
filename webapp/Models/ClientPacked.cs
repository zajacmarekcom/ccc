using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class ClientPacked
    {
        public int Id { get; set; }
        public int CementId { get; set; }
        public decimal BuyPrice { get; set; }
        public int PriceType { get; set; }
        public decimal SellPrice { get; set; }
    }
}