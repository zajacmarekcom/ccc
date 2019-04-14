using System.Collections.Generic;
using webapp.Models;
using webapp.Models.ViewModels;

namespace webapp.ViewModels
{
    public class DistributorViewModel
    {
        public string Name { get; set; }
        public bool IsMarket { get; set; }
        public MarketAddress Address { get; set; }
        public int Percent { get; set; }
        public ICollection<ViewManGroup> Groups { get; set; }
        public ICollection<ViewManufacturer> Manufacturers { get; set; }
    }
}