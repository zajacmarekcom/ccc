using System.Collections.Generic;

namespace webapp.ViewModels
{
    public class ManufacturersGroupViewModel
    {
        public string Name { get; set; }
        public int Percent { get; set; }
        public ICollection<ManufacturerViewModel> Manufacturers { get; set; }
    }
}