using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class ClientStep7
    {
        public int Id { get; set; }
        public ICollection<ClientLax> LaxTypes { get; set; }
        public ICollection<ClientPacked> PackedTypes { get; set; }
        public int VisitId { get; set; }
    }
}