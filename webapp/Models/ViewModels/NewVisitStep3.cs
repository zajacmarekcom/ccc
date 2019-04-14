using System.Collections.Generic;

namespace webapp.Models.ViewModels
{
    public class NewVisitStep3
    {
        public int VisitId { get; set; }
        public bool Cooperation { get; set; }
        public ICollection<SelectedSuppliers> Suppliers { get; set; }
        public ICollection<SelectedManufacturers> Manufacturers { get; set; }
        public ICollection<SelectedManufacturersGroups> ManufacturersGroups { get; set; }
        public ICollection<SelectedDistributor> Distributors { get; set; }
    }
}