using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class ClientStep5
    {
        public int Id { get; set; }
        public int VisitId { get; set; }
        public int Annual { get; set; }
        public virtual ICollection<VisitClients> Clients { get; set; }
    }
}