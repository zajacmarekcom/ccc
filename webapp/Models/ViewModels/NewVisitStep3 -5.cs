using System.Collections.Generic;

namespace webapp.Models.ViewModels
{
    public class Rec1
    {
        public IEnumerable<SelectedManufacturers> Manufacturers { get; set; }
        public ICollection<SelectedManufacturersGroups> ManufacturersGroups { get; set; }
    }

    public class Rec2
    {
        public ICollection<SelectedDistributor> Distributors { get; set; }
        public ICollection<SelectedManufacturers> Manufacturers { get; set; }
    }

    public class NewVisitStep3_5
    {
        public int VisitId { get; set; }
        public int ReceiptId { get; set; }
        public IEnumerable<Receipt> Receipts { get; set; }
        public Rec1 R1 { get; set; }
        public Rec2 R2 { get; set; }
    }
}