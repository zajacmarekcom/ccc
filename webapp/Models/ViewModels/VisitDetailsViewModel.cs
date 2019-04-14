using System.Collections.Generic;
using webapp.ViewModels;

namespace webapp.Models.ViewModels
{
    public class VisitDetailsViewModel
    {
        public string MainMarketSegment { get; set; }
        public int VisitId { get; set; }
        public string VisitDate { get; set; }
        public string Status { get; set; }
        public string VisitComments { get; set; }
        public bool Cooperation { get; set; }
        public IEnumerable<VisitMarketSegment> MarketSegments { get; set; }
        public IEnumerable<Branch> Branches { get; set; }
        public Assortment Assortment { get; set; }
        public IEnumerable<VisitLaxType> VisitLaxTypes { get; set; }
        public IEnumerable<VisitPackedType> VisitPackedTypes { get; set; }
        public Logistic Logistic { get; set; }
        public ICollection<string> UnloadTypes { get; set; }
        public ICollection<KeyValuePair<string, int>> Clients { get; set; }
        public ICollection<ViewPrice> LaxPrices { get; set; }
        public ICollection<ViewPrice> PackedPrices { get; set; }
        public ICollection<ViewSupplier> Suppliers { get; set; }
        public int PackageType { get; set; }
        public int LaxPercent { get; set; }
        public ICollection<ViewManufacturer> Step3Manufs { get; set; }
        public ICollection<ViewManGroup> Step3ManufsGroups { get; set; }
        public string RecType { get; set; }
        public ICollection<ViewManufacturer> Manufs { get; set; }
        public ICollection<ViewManGroup> ManGroups { get; set; }
        public ICollection<DistributorViewModel> Distributors { get; set; }
        public ClientDetails Data { get; set; }
        public ClientStep4 Step4 { get; set; }
        public ClientStep5 Step5 { get; set; }
        public ClientStep6 Step6 { get; set; }
        public ClientStep7 Step7 { get; set; }
        public ClientStep8 Step8 { get; set; }
    }
}