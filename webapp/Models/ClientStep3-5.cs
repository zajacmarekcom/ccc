using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class ClientStep3_5
    {
        public int Id { get; set; }
        public int VisitId { get; set; }
        public int ReceiptId { get; set; }
        public virtual ICollection<VisitManufacturers> Manufacturers { get; set; }
        public virtual ICollection<VisitManufacturersGroup> ManufacturersGroups { get; set; }
        public virtual ICollection<VisitDistributor> Distributors { get; set; }
    }
}