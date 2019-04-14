using System.Collections.Generic;

namespace webapp.Models.ViewModels
{
    public class ViewManGroup
    {
        public string Name { get; set; }
        public int Percent { get; set; }
        public ICollection<ViewManufacturer> Manufacturers { get; set; }
    }
}