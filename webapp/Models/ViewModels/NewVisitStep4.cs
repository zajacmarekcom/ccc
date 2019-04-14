using System.Collections.Generic;

namespace webapp.Models.ViewModels
{
    public class NewVisitStep4
    {
        public Logistic Logistic { get; set; }
        public IEnumerable<SelectedUnloadType> UnloadTypes { get; set; }
        public int VisitId { get; set; }
    }
}