using System.Collections.Generic;

namespace webapp.Models.ViewModels
{
    public class AddActionAdditionalData
    {
        public IEnumerable<District> Districts { get; set; }
        public IEnumerable<Province> Provinces { get; set; }
        public IEnumerable<Agent> Agents { get; set; }
        public IEnumerable<LegalForm> LegalForms { get; set; }
        public IEnumerable<Group> Groups { get; set; }
    }
}