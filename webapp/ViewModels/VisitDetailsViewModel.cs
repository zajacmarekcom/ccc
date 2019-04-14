using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using webapp.Models;
using webapp.Models.ViewModels;

namespace webapp.ViewModels
{
    public class VisitDetailsViewModel
    {
        public ClientDetailsViewModel ClientDetails { get; set; }
        public int Id { get; set; }
        public DateTime VisitDate { get; set; }
        public string VisitComments { get; set; }
        public string MainMarketSegment { get; set; }
        public IEnumerable<VisitMarketSegment> Segments { get; set; }
        public IEnumerable<BranchViewModel> Branches { get; set; }
        public bool Cooperation { get; set; }
        public IEnumerable<ViewSupplier> Suppliers { get; set; }
        public IEnumerable<ManufacturerViewModel> Manufacturers { get; set; }
        public IEnumerable<ManufacturersGroupViewModel> ManufacturersGroups { get; set; }
        public string ReceptionType { get; set; }
        public IEnumerable<ManufacturerViewModel> IndirectProducers { get; set; }
        public IEnumerable<ManufacturersGroupViewModel> IndirectProducersGroups { get; set; }
        public IEnumerable<DistributorViewModel> Distributors { get; set; }
        public AssortmentViewModel Assortment { get; set; }
        public IEnumerable<CementTypeViewModel> LaxTypes { get; set; }
        public IEnumerable<CementTypeViewModel> PackedTypes { get; set; }
        public LogisticViewModel Logistic { get; set; }
        [Display(Name = "Obrót roczny")]
        public string Annual { get; set; }
        public IEnumerable<string> Notes { get; set; }
        public IEnumerable<ClientViewModel> Clients { get; set; }
        public ClientStep6 Step6 { get; set; }
        public IEnumerable<ViewPrice> LaxPrices { get; set; }
        public IEnumerable<ViewPrice> PackedPrices { get; set; } 
    }
}
