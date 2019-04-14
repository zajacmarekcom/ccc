using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class ClientStep8
    {
        public int Id { get; set; }
        public int VisitId { get; set; }
        public int Status { get; set; }
        public DateTime? NewVisitDate { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
    }
}