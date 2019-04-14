using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class ClientStep4
    {
        public int Id { get; set; }
        public virtual Logistic Logistic { get; set; }
        public virtual ICollection<VisitUnloadType> UnloadTypes { get; set; }
        public int VisitId { get; set; }
    }
}