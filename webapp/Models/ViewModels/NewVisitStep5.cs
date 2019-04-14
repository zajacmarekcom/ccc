using System.Collections.Generic;

namespace webapp.Models.ViewModels
{
    public class NewVisitStep5
    {
        public int VisitId { get; set; }
        public int Annual { get; set; }
        public virtual IEnumerable<SelectedClients> Clients { get; set; }
    }
}